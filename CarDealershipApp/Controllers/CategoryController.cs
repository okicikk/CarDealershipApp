using CarDealership.ViewModels.Models.CategoryViewModels;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryServ)
        {
            this.categoryService = categoryServ;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await categoryService.GetAllCategoriesAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (await categoryService.DoesCategoryExistAsync(viewModel.Name))
            {
                TempData["ErrorMessage"] = "A category with this name already exists. Please choose another name.";
                return View(viewModel);
            }
            await categoryService.AddCategoryAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await categoryService.GetCategoryByIdAsync(id) is null)
            {
                return RedirectToAction(nameof(Index));
            }

            CategoryEditViewModel viewModel =  new CategoryEditViewModel()
            {
                Name = (await categoryService.GetCategoryByIdAsync(id)).Name,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditViewModel viewModel)
        {
            if (await categoryService.GetCategoryByIdAsync(viewModel.Id) is null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await categoryService.EditCategoryAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool IsDeleteSuccesfully = await categoryService.DeleteCategoryByIdAsync(id);
            if (!IsDeleteSuccesfully)
            {
                TempData["ErrorMessage"] = "Category could not be deleted.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Category deleted successfully, and cars reassigned to the default category.";
            return RedirectToAction(nameof(Index));
        }
    }
}
