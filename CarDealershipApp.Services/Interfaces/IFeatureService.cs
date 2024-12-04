using CarDealership.ViewModels.Models.FeatureViewModels;
using CarDealershipApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<IEnumerable<FeatureIndexViewModel>> GetAllFeaturesAsync();
        Task EditFeatureAsync(FeatureEditViewModel viewModel);
        Task<Feature?> GetFeatureByIdAsync(int id);
    }
}
