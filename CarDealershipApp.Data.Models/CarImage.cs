using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Data.Models
{
    public class CarImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FilePath { get; set; } = string.Empty;

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public Car Car { get; set; }
    }

}
