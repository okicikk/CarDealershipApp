using CarDealershipApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Diagnostics;

namespace CarDealershipApp.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (!statusCode.HasValue)
            {
                return this.View("Error");
            }
            if (statusCode == 404)
            {
                return this.View("404");
            }
            else if (statusCode == 403 || statusCode == 401 || statusCode == 405)
            {
                return this.View("403");
            }
            else
            {
                return this.View("500");
            }

        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}