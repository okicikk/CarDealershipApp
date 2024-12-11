using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Data.Models;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IModelService
    {
        Task<bool> SoftDeleteByIdAsync(int id);
        Task EditModel(ModelEditViewModel viewModel);
		Task<(List<ModelIndexViewModel> Models, int TotalPages)> GetAllModelsAsync(int pageNumber = 1, int pageSize = 10);
        Task<ModelEditViewModel> InitializeModelByIdAsync(int id);

        Task AddModelAsync(ModelAddViewModel viewModel);
        Task<IList<Brand>>GetAllBrandsAsync();
    }
}
