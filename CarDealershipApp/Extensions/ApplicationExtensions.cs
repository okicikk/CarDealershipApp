using Microsoft.AspNetCore.Identity;

namespace CarDealershipApp.Web.Extensions
{
	public static class ApplicationExtensions
	{
		public static async Task SeedRolesAndAdminUserAsync(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

				var roles = new[] { "Admin" };

				// Seed roles
				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new IdentityRole(role));
					}
				}

				// Seed admin user
				var adminEmail = "admin@gmail.com";
				var adminPassword = "AdminPass";

				var adminUser = await userManager.FindByEmailAsync(adminEmail);
				if (adminUser == null)
				{
					adminUser = new IdentityUser
					{
						UserName = adminEmail,
						Email = adminEmail
					};

					var createResult = await userManager.CreateAsync(adminUser, adminPassword);
					if (createResult.Succeeded)
					{
						await userManager.AddToRoleAsync(adminUser, "Admin");
					}
				}
			}
		}
	}
}
