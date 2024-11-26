using CarDealershipApp.Models.CarViewModels;
using CarDealershipApp.Services.Interfaces;
using CarDealershipApp.Infrastructure.Repositories;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace CarDealershipApp.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepository;
        private readonly IRepository<Brand>brandRepository;
        private readonly IRepository<Category> categoryRepository;

        public CarService
            (IRepository<Car> repository,
            IRepository<Brand>brandRepository,
            IRepository<Category> categoryRepository)
        {
            this.carRepository = repository;
            this.brandRepository = brandRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task AddCarAsync(CarAddViewModel viewModel)
        {
            viewModel.Brands = await brandRepository
                .GetAllQueryable()
                .Include(x=>x.Models)
                .Where(x=>x.IsDeleted)
                .ToListAsync();

            Brand? brand = await brandRepository.GetByIdAsync(viewModel.BrandId);

            if (brand is null)
            {
                throw new ArgumentException("Select one of the available brands!");
            }

            viewModel.Models = brand
                .Models
                .ToList();

            Model? brandModel = viewModel.Models.FirstOrDefault(x=>x.Id == viewModel.ModelId);

			if (brandModel is null)
			{
				throw new ArgumentException("Select one of the available models!");
			}

            viewModel.Categories = await categoryRepository.GetAllAsync();

			Car carToAdd = new Car()
            {
                Brand = brand,
                Model = brandModel,
                CategoryId = viewModel.CategoryId,
                Price = viewModel.Price,
                Weight = viewModel.Weight,
                Description = viewModel.Description ?? "No description!",
                Mileage = viewModel.Mileage,
                ImageUrls = viewModel.ImageUrls,
                ReleaseYear = viewModel.ReleaseYear,
                ListedOn = viewModel.ListedOn,
                CarsFeatures = viewModel.CarsFeatures
            };
            await carRepository.AddAsync(carToAdd);
        }
    }
}
