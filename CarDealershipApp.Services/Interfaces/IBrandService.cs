using CarDealership.Web.ViewModels.Models.Brand;
using CarDealershipApp.Data.Models;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IBrandService
    {
        Task AddBrandAsync(BrandAddViewModel model);
        Task<List<BrandIndexViewModel>> GetAllBrandsAsync();
        Task<bool> SoftDeleteByIdAsync(int id);
        Task EditAsync(BrandIndexViewModel viewModel);
        Task<Brand> GetByIdAsync(int id);
    }
}
