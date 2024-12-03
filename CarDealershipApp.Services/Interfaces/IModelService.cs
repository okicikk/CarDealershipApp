using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Data.Models;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IModelService
    {
        Task<bool> SoftDeleteByIdAsync(int id);
        Task EditModel(ModelEditViewModel viewModel);
        Task<IEnumerable<ModelIndexViewModel>> GetAllModelsAsync();
        Task<ModelEditViewModel> InitializeModelByIdAsync(int id);

        Task AddModelAsync(ModelAddViewModel viewModel);
        Task<IList<Brand>>GetAllBrandsAsync();
    }
}
