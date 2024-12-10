
namespace CarDealership.ViewModels.Models.UserCarViewModels
{
	public class UserCarIndexViewModel
	{
        public string UserId { get; set; }
        public int CarId { get; set; }

        public string CarName { get; set; }
        public string CarPrice { get; set; }

        public string? ImageUrl { get; set; }
    }
}
