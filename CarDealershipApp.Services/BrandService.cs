using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Models.Brand;
using CarDealershipApp.Services.Interfaces;


namespace CarDealershipApp.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brandRepository;

        public BrandService(IRepository<Brand> repository)
        {
            brandRepository = repository;
        }
        public async Task AddBrandAsync(BrandAddViewModel model)
        {
            Brand brand = new Brand()
            {
                Name = model.Name,
                Models = new List<Model>()
            };

            if (model.Model is not null)
            {
                brand.Models.Add(new Model()
                {
                    Name = model.Model,
                    BrandId = brand.Id
                });
            }
            await brandRepository.AddAsync(brand);
        }
    }
}
