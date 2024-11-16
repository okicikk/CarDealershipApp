using Microsoft.AspNetCore.Mvc;
using CarDealershipApp;
using CarDealershipApp.Services.Interfaces;
using CarDealershipApp.Models.Brand;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using CarDealershipApp.Data.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace CarDealershipApp.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;
        public BrandController(IBrandService _brandService)
        {
            brandService = _brandService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BrandAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await brandService.AddBrandAsync(viewModel);
            return RedirectToPage("/Home");
        }
    }
}
