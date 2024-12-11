
using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarDealershipApp.Services
{
    public class ModelService : IModelService
    {
        private readonly IRepository<Model> modelRepository;
        private readonly IRepository<Brand> brandRepository;
        private readonly ICarService carService;


        public ModelService(IRepository<Model> modelRep,
            IRepository<Brand> brandrep,
            ICarService carService)
        {
            this.modelRepository = modelRep;
            this.brandRepository = brandrep;
            this.carService = carService;
        }
		public async Task<(List<ModelIndexViewModel> Models, int TotalPages)> GetAllModelsAsync(int pageNumber = 1, int pageSize = 10)
		{
			var modelsQuery = modelRepository
				.GetAllQueryable()
				.Where(x => x.IsDeleted == false)
				.Include(x => x.Brand)
				.OrderBy(x => x.Brand.Name)
				.ThenBy(x => x.Name);

			int totalModels = await modelsQuery.CountAsync();
			int totalPages = (int)Math.Ceiling(totalModels / (double)pageSize);

			var models = await modelsQuery
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			var viewModel = new List<ModelIndexViewModel>();

			foreach (var model in models)
			{
				var carsCount = await carService.GetCarsCountWithModelId(model.Id);

				viewModel.Add(new ModelIndexViewModel()
				{
					Id = model.Id,
					Name = model.Name,
					BrandId = model.BrandId,
					BrandName = model.Brand.Name,
					CarsCount = carsCount,
				});
			}

			return (viewModel, totalPages);
		}

		public async Task EditModel(ModelEditViewModel viewModel)
        {
            int id = viewModel.Id;
            Model model = await modelRepository.GetByIdAsync(id);

            if (model == null)
            {
                throw new ArgumentException("No such model");
            }
            model.Name = viewModel.Name;
            await modelRepository.UpdateAsync(model);
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
                (x => x.Name.ToLower() == modelToBeAdded.Name.ToLower()
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
        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            Model model = await modelRepository.GetByIdAsync(id);
            if (model.IsDeleted == false)
            {
                model.IsDeleted = true;
                
                await carService.SoftDeleteCarsByModelId(id);

                modelRepository.Update(model);
                return true;
            }
            return false;
        }
        public async Task<ModelEditViewModel> InitializeModelByIdAsync(int id)
        {
            Model model = await modelRepository.GetByIdAsync(id);
            if (model is null)
            {
                throw new ArgumentException("No such model");
            }
            ModelEditViewModel viewModel = new ModelEditViewModel()
            {
                Id = id,
                Name = model.Name,
            };
            return viewModel;
        }
    }
}
