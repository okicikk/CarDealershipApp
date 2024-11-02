using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipApp.Data.Models
{
    public class CarFeature
    {
        [Required]
        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }
        [Required]
        public int FeatureId { get; set; }
        [ForeignKey(nameof(FeatureId))]
        public Feature Feature { get; set; }
    }
}
