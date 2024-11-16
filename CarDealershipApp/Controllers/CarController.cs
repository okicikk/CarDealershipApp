using CarDealershipApp.Models.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace CarDealershipApp.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly 
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CarAddViewModel carToAdd)
        {
            return View();
        }
    }
}
