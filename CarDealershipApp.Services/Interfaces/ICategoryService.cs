using CarDealership.ViewModels.Models.CategoryViewModels;
using CarDealershipApp.Data.Models;

namespace CarDealershipApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryIndexViewModel>> GetAllCategoriesAsync();
        Task<bool> DeleteCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryAddViewModel viewModel);
        Task<bool> DoesCategoryExistAsync(string name);
        Task EditCategoryAsync(CategoryEditViewModel viewModel);
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
