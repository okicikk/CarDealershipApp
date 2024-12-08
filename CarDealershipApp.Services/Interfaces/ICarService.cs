using CarDealership.ViewModels.Models.Car;
using CarDealershipApp.Data.Models;
using CarDealershipApp.ViewModels.CarViewModels;

namespace CarDealershipApp.Services.Interfaces
{
	public interface ICarService
	{
		Task<int> GetCarsCountWithModelId(int modelId);
		Task<List<CarPreview>> CheckYourCars(string userId);

		Task<string> GetCarOwnerIdAsync(int carId);

		Task AddCarAsync(CarAddViewModel model);
		Task SoftDeleteCarsByModelId(int modelId);
		Task<bool> SoftDeleteAsync(int carId);

		//void AddCar(CarAddViewModel model);
		Task<List<Brand>> GetAllBrandsAsync();

		Task<CarAddViewModel> InitializeCarAddViewModelAsync(int brandId, string userId);
		Task<List<CarPreview>> CheckAllCarsAsync(string? brandName = null,
												 string? modelName = null,
												 string? category = null,
												 int? minReleaseYear = null,
												 int? maxReleaseYear = null);

		Task<CarDetailsViewModel> LoadDetailsAsync(int carId);
		Task EditCarAsync(CarEditViewModel viewModel);
		Task<CarEditViewModel> InitializeCarEditViewModel(int carId);


	}
}
