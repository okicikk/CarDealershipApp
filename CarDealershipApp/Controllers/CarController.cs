using Microsoft.AspNetCore.Mvc;

namespace CarDealershipApp.Controllers
{
	public class CarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
