using CarDealership.ViewModels.Models.Car;
using CarDealershipApp.Data.Models;
using CarDealershipApp.ViewModels.CarViewModels;

namespace CarDealershipApp.Services.Interfaces
{
	public interface ICarService
	{
		Task AddCarAsync(CarAddViewModel model);
		Task<bool> SoftDeleteAsync(int carId);

		void AddCar(CarAddViewModel model);
		Task<List<Brand>> GetAllBrandsAsync();

		Task<CarAddViewModel> InitializeCarAddViewModelAsync(int brandId, string userId);
		Task<List<CarPreview>> CheckAllCarsAsync();

		Task<CarDetailsViewModel> LoadDetailsAsync(int carId);
		Task EditCarAsync(CarEditViewModel viewModel);
		Task<CarEditViewModel> InitializeCarEditViewModel(int carId);


    }
}
