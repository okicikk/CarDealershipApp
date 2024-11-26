
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

            Model? existingModel = await modelRepository
                .GetAllQueryable()
                .FirstOrDefaultAsync
                (x=>x.Name.ToLower() == modelToBeAdded.Name.ToLower()
                        && x.BrandId == modelToBeAdded.BrandId);

            if (existingModel is not null)
            {
                throw new ArgumentException("Model with that name already exists!");
            }

            await modelRepository.AddAsync(modelToBeAdded);
        }

        public async Task<IList<Brand>> GetAllBrandsAsync()
        {
            return await brandRepository
                .GetAllQueryable()
                .ToListAsync();
        }
    }
}
