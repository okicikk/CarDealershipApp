

using CarDealership.ViewModels.Models.UserCarViewModels;
using CarDealershipApp.Data.Models;

namespace CarDealershipApp.Services.Interfaces
{
	public interface IUserCarService
	{
		public Task<IEnumerable<UserCarIndexViewModel>> LoadUserCarsAsync(string userId);
		public Task AddAsync(int carId, string userId);

		public Task<bool> DeleteAsync(int carId,string userId);

		public Task<bool> DeleteByUserAsync(string userId);
		public Task<bool> DeleteByCarAsync(int carId);

	}
}
