using CarDealership.ViewModels.Models.Car;
using CarDealershipApp.Data.Models;
using CarDealershipApp.ViewModels.CarViewModels;

namespace CarDealershipApp.Services.Interfaces
{
    public interface ICarService
    {
        Task AddCarAsync(CarAddViewModel model);
        void AddCar(CarAddViewModel model);
        Task<List<Brand>> GetAllBrandsAsync();

        Task<CarAddViewModel> InitializeCarAddViewModelAsync(int brandId,string userId);
        Task<List<CarPreview>> CheckAllCarsAsync();

        Task<CarDetailsViewModel> LoadDetailsAsync(int carId);

    }
}
