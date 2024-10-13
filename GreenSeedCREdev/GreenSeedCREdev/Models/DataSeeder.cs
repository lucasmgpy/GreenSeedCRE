using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GreenSeedCREdev.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace GreenSeedCREdev.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // Verificar se o role ADMIN existe
            if (!await roleManager.RoleExistsAsync("ADMIN"))
            {
                var adminRole = new IdentityRole("ADMIN");
                await roleManager.CreateAsync(adminRole);
            }

            // Verificar se o utilizador administrador existe
            string adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "ADMIN");
                }
                else
                {
                    // Tratar erros
                    throw new Exception("Falha ao criar utilizador administrador: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Garantir que o utilizador está no role ADMIN
                if (!await userManager.IsInRoleAsync(adminUser, "ADMIN"))
                {
                    await userManager.AddToRoleAsync(adminUser, "ADMIN");
                }
            }
        }

    }
}
