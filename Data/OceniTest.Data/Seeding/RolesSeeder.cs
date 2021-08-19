namespace OceniTest.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using OceniTest.Common;
    using OceniTest.Data.Models;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var roleId = await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedAdministrator(dbContext, roleId);
        }

        private static async Task<string> SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                role = await roleManager.FindByNameAsync(roleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                return role.Id;
            }

            return role.Id;
        }

        private static async Task SeedAdministrator(ApplicationDbContext dbContext, string roleId)
        {
            var passwordHasher = new PasswordHasher<string>();

            var admin = new ApplicationUser()
            {
                Email = "admin@abv.bg",
                NormalizedEmail = "ADMIN@ABV.BG",
                UserName = "admin@abv.bg",
                NormalizedUserName = "ADMIN@ABV.BG",
                IsMember = true,
                PasswordHash = passwordHasher.HashPassword(string.Empty, "123456"),
            };

            var userInRole = new IdentityUserRole<string>() { RoleId = roleId, UserId = admin.Id };

            await dbContext.Users.AddAsync(admin);
            await dbContext.UserRoles.AddAsync(userInRole);

            await dbContext.SaveChangesAsync();
        }
    }
}
