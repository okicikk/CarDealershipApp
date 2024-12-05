using CarDealership.ViewModels.Models.FeatureViewModels;
using CarDealershipApp.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IFeatureService
    {
        Task AddAsync(FeatureAddViewModel viewModel);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<FeatureIndexViewModel>> GetAllFeaturesAsync();
        Task EditFeatureAsync(FeatureEditViewModel viewModel);
        Task<Feature?> GetFeatureByIdAsync(int id);
    }
}
