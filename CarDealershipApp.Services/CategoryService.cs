using CarDealership.ViewModels.Models.CategoryViewModels;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CarDealershipApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Car> carRepository;

        public CategoryService(IRepository<Category> categoryRepo, IRepository<Car> carRepository)
        {
            this.categoryRepository = categoryRepo;
            this.carRepository = carRepository;
        }

        public async Task AddCategoryAsync(CategoryAddViewModel viewModel)
        {
            
            if (viewModel is null)
            {
                throw new ArgumentNullException("Category is null");
            }
            Category category = new Category()
            {
                Name = viewModel.Name,
            };
            await categoryRepository.AddAsync(category);
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            Category categoryToBeDeleted = await categoryRepository.GetByIdAsync(id);
            if (categoryToBeDeleted == null)
            {
                return false;
            }

            var defaultCategory = await categoryRepository
                .GetAllQueryable()
                .FirstOrDefaultAsync(c => c.Name == "No Category");

            if (defaultCategory == null)
            {
                defaultCategory = new Category { Name = "No Category" };
                await categoryRepository.AddAsync(defaultCategory);
            }
            List<Car> carsWithCategory = await carRepository
                .GetAllQueryable()
                .Where(x=>x.CategoryId == id)
                .ToListAsync();
            foreach (Car car in carRepository.GetAll())
            {
                car.Category = defaultCategory;
                carRepository.Update(car);
            }
            await categoryRepository.DeleteByIdAsync(id);
            return true;
        }

        public async Task<bool> DoesCategoryExistAsync(string name)
        {
            if (await categoryRepository.GetAllQueryable().AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CategoryIndexViewModel>> GetAllCategoriesAsync()
        {
            List<CategoryIndexViewModel> viewModels = await categoryRepository
                .GetAllQueryable()
                .Select(x => new CategoryIndexViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return viewModels;
        }
    }
}
