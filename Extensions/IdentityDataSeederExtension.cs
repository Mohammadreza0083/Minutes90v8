using Microsoft.AspNetCore.Identity;
using minutes90.Entities;
using minutes90.Entities.Roles;

namespace minutes90.Extensions
{
    public class IdentityDataSeederExtension
    {
        public static async Task SeedUsersAndRolesAsync(UserManager<AppUsers> userManager,
        RoleManager<AppRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new AppRole { Name = "User" });
            }

            if (await userManager.FindByEmailAsync("info@minutes90.com") == null)
            {
                var adminUser = new AppUsers()
                {
                    Email = "info@minutes90.com",
                    UserName = "Admin",
                    DisplayName = "Admin"
                };
                var result = await userManager.CreateAsync(adminUser, "Pa$$w0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
