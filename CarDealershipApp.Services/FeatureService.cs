using CarDealership.ViewModels.Models.FeatureViewModels;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarDealershipApp.Services
{
	public class FeatureService : IFeatureService
	{
		private readonly IRepository<Feature> featureRepository;
        public FeatureService(IRepository<Feature> featureRepo)
        {
            this.featureRepository = featureRepo;
        }

		public async Task<bool> DeleteByIdAsync(int id)
		{
			return await featureRepository.DeleteByIdAsync(id);
		}
        public async Task<IEnumerable<FeatureIndexViewModel>> GetAllFeaturesAsync()
		{
			return await featureRepository
				.GetAllQueryable()
				.Select(x => new FeatureIndexViewModel()
				{
					Id = x.Id,
					Name = x.Name,
				})
				.OrderBy(x => x.Name)
				.ToListAsync();
		}
		public async Task EditFeatureAsync(FeatureEditViewModel viewModel)
		{
            Feature feature = await featureRepository.GetByIdAsync(viewModel.Id);

			feature.Name = viewModel.Name;

			featureRepository.Update(feature);

		}

		public async Task<Feature?> GetFeatureByIdAsync(int id)
		{
			return await featureRepository
				.GetAllQueryable()
				.Include(x => x.CarsFeatures)
				.FirstOrDefaultAsync(x=>x.Id == id);
		}

		public async Task AddAsync(FeatureAddViewModel viewModel)
		{
			if (await featureRepository
				.GetAllQueryable()
				.AnyAsync(x=>x.Name == viewModel.Name))
			{
				throw new ArgumentException("Feature already exists.");
			}
			Feature featureToAdd = new Feature()
			{
				Name = viewModel.Name,
			};
			await featureRepository.AddAsync(featureToAdd);
		}
	}
}
