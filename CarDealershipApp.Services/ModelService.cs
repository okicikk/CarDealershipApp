
using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace CarDealershipApp.Services
{
    public class ModelService : IModelService
    {
        private readonly IRepository<Model> modelRepository;
        private readonly IRepository<Brand> brandRepository;


        public ModelService(IRepository<Model> modelRep,IRepository<Brand> brandrep)
        {
            this.modelRepository = modelRep;
            this.brandRepository = brandrep;
        }

        public async Task AddModelAsync(ModelAddViewModel viewModel)
        {
            viewModel.Brands = await brandRepository.GetAllAsync();
            Model modelToBeAdded = new Model()
            {
                Name = viewModel.Name,
                BrandId = viewModel.BrandId,
            };
            await modelRepository.AddAsync(modelToBeAdded);

            if (modelRepository.GetAllQueryable().Any(x => x.Name.ToLower() == modelToBeAdded.Name.ToLower()))
            {
                throw new ArgumentException("Model with that name already exists!");
            }
        }

        public async Task<IList<Brand>> GetAllBrandsAsync()
        {
            return await brandRepository
                .GetAllQueryable()
                .ToListAsync();
        }
    }
}
