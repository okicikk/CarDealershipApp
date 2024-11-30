using CarDealershipApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.ViewModels.Models.Car
{
    public class CarAddBrandSelectionViewModel
    {
        public IEnumerable<Brand> Brands { get; set; } = new List<Brand>();

        [Range(1, int.MaxValue, ErrorMessage = "Select a valid brand!")]
        public int SelectedBrandId { get; set; } 
    }
}
