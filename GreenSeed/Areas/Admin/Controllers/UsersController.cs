using GreenSeed.Areas.Admin.Models;
using GreenSeed.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSeed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Listar todos os usuários
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // Editar usuário (atribuir papéis)
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
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
                Email = user.Email,
                Roles = userRoles,
                AllRoles = _roleManager.Roles.Select(r => r.Name).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Email = model.Email;
                user.UserName = model.Email;

                var userRoles = await _userManager.GetRolesAsync(user);

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Não foi possível atualizar o usuário.");
                    model.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList(); // Popula AllRoles
                    return View(model);
                }

                // Remover roles existentes
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                // Adicionar novos roles
                if (selectedRoles != null)
                {
                    await _userManager.AddToRolesAsync(user, selectedRoles);
                }

                TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            // Popula AllRoles quando ModelState não é válido
            model.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(model);
        }

        // Excluir usuário
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                TempData["SuccessMessage"] = "Usuário excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
