using CarDealershipApp.Models.CarViewModels;

namespace CarDealershipApp.Services.Interfaces
{
    public interface ICarService
    {
        Task AddCarAsync(CarAddViewModel model);

    }
}
