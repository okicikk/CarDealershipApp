using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealership.Web.ViewModels.Models.Brand;
using CarDealershipApp.Services.Interfaces;
using static CarDealershipApp.Constants.Constants;
using Microsoft.EntityFrameworkCore;


namespace CarDealershipApp.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brandRepository;
        private readonly IRepository<Model> modelRepository;
        private readonly IRepository<Car> carRepository;
        private readonly IRepository<UserCar> usersCarsRepository;

        public BrandService(IRepository<Brand> brandRepository, IRepository<Model>modelRepository, IRepository<Car> carRepository, IRepository<UserCar> usersCarsRepo)
        {
            this.brandRepository = brandRepository;
            this.modelRepository = modelRepository;
            this.carRepository = carRepository;
            this.usersCarsRepository = usersCarsRepo;
        }
        public async Task AddBrandAsync(BrandAddViewModel model)
        {
            Brand brand = new Brand()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl ?? DefaultBrandImage,
                Models = new List<Model>()
            };

            if (brandRepository.GetAllQueryable().Any(x => x.Name.ToLower() == brand.Name.ToLower()))
            {
                throw new ArgumentException("Brand with that name already exists!");
            }
            await brandRepository.AddAsync(brand);
        }
        public async Task EditAsync(BrandIndexViewModel viewModel)
        {
            int id = viewModel.Id;
            Brand brand = await brandRepository.GetByIdAsync(id);
            brand.Name = viewModel.Name;
            brand.ImageUrl = viewModel.ImageUrl;
            await brandRepository.UpdateAsync(brand);
        }
        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            Brand brand = await brandRepository.GetByIdAsync(id);
            if (brand.IsDeleted == false)
            {
                brand.IsDeleted = true;
                List<Model>modelsToBeDeleted = await modelRepository.GetAllQueryable()
                    .Where(x=>x.BrandId == id)
                    .ToListAsync();

                List<Car> carsToBeDeleted = await carRepository
                    .GetAllQueryable()
                    .Where (x=>x.BrandId == id)
                    .ToListAsync();
                List<UserCar> usersCarsToBeDeleted = await usersCarsRepository
                    .GetAllQueryable()
                    .Where(x=>x.Car.BrandId == id)
                    .ToListAsync();

                foreach (var userCar in usersCarsToBeDeleted)
                {
                    await usersCarsRepository.DeleteAsync(userCar);
                }

                foreach (Model model in modelsToBeDeleted)
                {
                    model.IsDeleted = true;
                    await modelRepository.UpdateAsync(model);
                }
                foreach (Car car in carsToBeDeleted)
                {
                    car.IsDeleted = true;
                }
                await brandRepository.UpdateAsync(brand);

                return true;
            }
            return false;
        }

        public async Task<List<BrandIndexViewModel>> GetAllBrandsAsync()
        {
            List<BrandIndexViewModel> brandIndexViewModels = await brandRepository
                .GetAllQueryable()
                .Where(x => x.IsDeleted == false)
                .Select(x => new BrandIndexViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            return brandIndexViewModels;

        }

        public Task<Brand> GetByIdAsync(int id)
        {
            return brandRepository.GetByIdAsync(id);
        }
    }
}
