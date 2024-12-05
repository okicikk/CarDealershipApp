using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarDealershipApp.Data;
using CarDealershipApp.Services.Interfaces;
using CarDealershipApp.Services;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Infrastructure.Repositories;
using CarDealershipApp.Web.Extensions;
namespace CarDealershipApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

			builder.Services.AddDbContext<CarDealershipDbContext>(options => options.UseSqlServer(connectionString));


			builder.Services.AddDefaultIdentity<IdentityUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
			})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<CarDealershipDbContext>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			builder.Services.AddScoped<IBrandService, BrandService>();
			builder.Services.AddScoped<IModelService, ModelService>();
			builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IFeatureService, FeatureService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();




            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}



			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapRazorPages();

			await app.SeedRolesAndAdminUserAsync();

			app.Run();
		}
	}
}
