using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static CarDealershipApp.Constants.Constants;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.ViewModels.Models.FeatureViewModels
{
    public class FeatureEditViewModel
    {
        public int Id { get; set; }
        [MinLength(FeatureNameMinLength)]
        [MaxLength(FeatureNameMaxLength)]
        [Required]
        public string Name { get; set; }
        public int CarsWithFeatureCount { get; set; }
    }
}
