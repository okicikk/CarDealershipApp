using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Data.Models;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IModelService
    {
        Task AddModelAsync(ModelAddViewModel viewModel);
        Task<IList<Brand>>GetAllBrandsAsync();
    }
}
