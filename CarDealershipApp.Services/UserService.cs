using CarDealership.ViewModels.Models.Admin;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository<Car> carRepository;
        public UserService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRepository<Car> carRepo)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.carRepository = carRepo;
        }

        public async Task<bool> AssignRoleToUserAsync(string id, string role)
        {
            IdentityUser? user = await userManager.FindByIdAsync(id);

            if (user is null)
            {
                return false;
            }

            bool roleExists = await roleManager.RoleExistsAsync(role);

            if (roleExists == false)
            {
                return false;
            }
            if (await userManager.IsInRoleAsync(user, role))
            {
                return false;
            }

            IdentityResult result = await userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }
        public async Task<bool> RemoveRoleFromUserAsync(string id, string role)
        {
            IdentityUser? user = await userManager.FindByIdAsync(id);

            if (user is null)
            {
                return false;
            }

            bool roleExists = await roleManager.RoleExistsAsync(role);

            if (roleExists == false)
            {
                return false;
            }
            bool isInRole = await userManager.IsInRoleAsync(user, role);

            if (isInRole == false)
            {
                return false;
            }
            IdentityResult result = await userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded;
        }
        public async Task<IEnumerable<UsersAllViewModel>> GetAllUsersAsync()
        {
            IEnumerable<IdentityUser> users = await userManager.Users.ToListAsync();

            List<UsersAllViewModel> usersAllViewModel = new List<UsersAllViewModel>();

            foreach (var user in await userManager.Users.ToListAsync())
            {
                usersAllViewModel.Add(new UsersAllViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = await userManager.GetRolesAsync(user),
                });
            }
            return usersAllViewModel;
        }

        public async Task<bool> DeleteUserByIdAsync(string id)
        {
            IdentityUser? user = await userManager.FindByIdAsync(id);

            if (user is null || user.Email == DefaultUserEmail)
            {
                return false;
            }

            List<Car> carsWithUserToBeDeleted = await carRepository
                .GetAllQueryable()
                .Where(x => x.SellerId == id)
                .ToListAsync();

            foreach (var car in carsWithUserToBeDeleted)
            {
                if (!car.IsDeleted)
                {
                    car.IsDeleted = true;
                }

                var defaultSeller = await userManager.FindByEmailAsync(DefaultUserEmail);
                if (defaultSeller == null)
                {
                    return false;
                }

                car.Seller = defaultSeller;
                await carRepository.UpdateAsync(car);
            }

            var isSuccess = await userManager.DeleteAsync(user);
            return isSuccess.Succeeded;

        }
    }
}
