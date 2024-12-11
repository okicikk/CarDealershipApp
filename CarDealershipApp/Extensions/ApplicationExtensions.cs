using Microsoft.AspNetCore.Identity;
using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Web.Extensions
{
    public static class ApplicationExtensions
    {
        public static async Task SeedRolesAndUsersAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var roles = new[] { "Admin", "User" };

                // Seed roles
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Seed admin user
                string adminEmail = "admin@gmail.com";
                string adminPassword = "AdminPass";

                // Seed default user for cars with deleted users and for seeding
                string defaultEmail = DefaultUserEmail;
                string defaultPass = "DefaultPass";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                var defaultUser = await userManager.FindByEmailAsync(defaultEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        Id = "8530b19f-939d-4fd8-b4b0-d778de91ab50",
                        UserName = adminEmail,
                        Email = adminEmail
                    };

                    var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                        await userManager.AddToRoleAsync(adminUser, "User");
                    }
                }
				else if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
				{
					await userManager.AddToRoleAsync(adminUser, "Admin");
				}
				if (defaultUser is null)
                {
                    defaultUser = new IdentityUser()
                    {
                        Id = "4e32b02a-046c-40be-bfeb-327c900e6bb9",
                        UserName = defaultEmail,
                        Email = defaultEmail
                    };
                    var createResult = await userManager.CreateAsync(defaultUser, defaultPass);
                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, "User");
                    }
                }
                else if (!await userManager.IsInRoleAsync(defaultUser, "User"))
                {
                        await userManager.AddToRoleAsync(defaultUser, "User");
				}
			}
        }
    }
}
