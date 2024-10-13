using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GreenSeedCREdev.Models;
using GreenSeedCREdev.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace GreenSeedCREdev.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: UserManagement
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: UserManagement/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles.ToList()
            };

            ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            return View(model);
        }

        // POST: UserManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;

                var userRoles = await _userManager.GetRolesAsync(user);
                selectedRoles = selectedRoles ?? new string[] { };

                var rolesToAdd = selectedRoles.Except(userRoles).ToList();
                var rolesToRemove = userRoles.Except(selectedRoles).ToList();

                // Prevenir que um administrador remova o seu próprio role ADMIN
                if (user.Id == _userManager.GetUserId(User) && rolesToRemove.Contains("ADMIN"))
                {
                    ModelState.AddModelError("", "Você não pode remover o seu próprio role ADMIN.");
                    ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                    return View(model);
                }

                if (rolesToAdd.Any())
                {
                    var addRoleResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    if (!addRoleResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Não foi possível adicionar os roles.");
                        return View(model);
                    }
                }

                if (rolesToRemove.Any())
                {
                    var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    if (!removeRoleResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Não foi possível remover os roles.");
                        return View(model);
                    }
                }

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(model);
        }

        // GET: UserManagement/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id == _userManager.GetUserId(User))
            {
                return Forbid(); // Prevenir que um administrador delete a si mesmo
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new DeleteUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        // POST: UserManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null || id == _userManager.GetUserId(User))
            {
                return Forbid(); // Prevenir que um administrador delete a si mesmo
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Remover o usuário de todos os roles antes de deletá-lo
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeRolesResult.Succeeded)
                {
                    ModelState.AddModelError("", "Não foi possível remover os roles do utilizador.");
                    var model = new DeleteUserViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email

                    };
                    return View(model);
                }
            }

            var deleteResult = await _userManager.DeleteAsync(user);

            if (deleteResult.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Não foi possível deletar o utilizador.");
                var model = new DeleteUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return View(model);
            }
        }
    }
}
