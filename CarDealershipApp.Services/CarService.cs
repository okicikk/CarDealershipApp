using CarDealershipApp.Models.CarViewModels;
using CarDealershipApp.Services.Interfaces;
using CarDealershipApp.Infrastructure.Repositories;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Data.Models;
using System.CodeDom;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipApp.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepository;

        public CarService(IRepository<Car> repository)
        {
            carRepository = repository;
        }

        public Task AddCarAsync(CarAddViewModel model)
        {
            Car carToAdd = new Car()
            {
                Brand = model.Brand,

            };
            await carRepository.AddAsync
        }
    }
}
