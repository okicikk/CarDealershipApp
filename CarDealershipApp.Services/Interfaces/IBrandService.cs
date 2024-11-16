using CarDealershipApp.Models.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IBrandService
    {
        Task AddBrandAsync(BrandAddViewModel model);
    }
}
