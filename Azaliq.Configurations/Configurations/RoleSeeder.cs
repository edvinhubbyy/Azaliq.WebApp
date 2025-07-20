using Azaliq.Data.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Azaliq.Data.Configurations
{
    public static class RoleSeeder
    {
        public static void AssignRoles(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            EnsureRoleExists(roleManager, "Admin");
            EnsureRoleExists(roleManager, "Manager");
            EnsureRoleExists(roleManager, "User"); 

            SeedUser(userManager, "admin@example.com", "Admin@123", "Admin");
        }


        private static void EnsureRoleExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var exists = roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();
            if (!exists)
            {
                var result = roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role: {roleName}");
                }
            }
        }

        private static void SeedUser(UserManager<ApplicationUser> userManager, string email, string password, string role)
        {
            var user = userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };

                var createUserResult = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create user: {email}");
                }
            }

            var isInRole = userManager.IsInRoleAsync(user, role).GetAwaiter().GetResult();
            if (!isInRole)
            {
                var addRoleResult = userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign {role} role to user: {email}");
                }
            }
        }
    }
}
