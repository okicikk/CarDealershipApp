using CarDealership.ViewModels.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryIndexViewModel>> GetAllCategoriesAsync();
        Task<bool> DeleteCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryAddViewModel viewModel);
        Task<bool> DoesCategoryExistAsync(string name);
    }
}
