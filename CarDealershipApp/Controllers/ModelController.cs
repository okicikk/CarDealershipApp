using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Services;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipApp.Web.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelService modelService;
        public ModelController(IModelService modelServ)
        {
            this.modelService = modelServ;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ModelAddViewModel viewModel = new ModelAddViewModel();
            viewModel.Brands = await modelService.GetAllBrandsAsync();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Add(ModelAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
