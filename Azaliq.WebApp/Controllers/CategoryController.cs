using Azaliq.Services.Core;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();
            var products = await _categoryService.GetAllCategoriesAsync(userId);

            // TODO: Fix this
            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new AddCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _categoryService.AddCategoryAsync(model);
            return RedirectToAction("Index", "Home"); // or to a list of categories
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                    return RedirectToAction(nameof(Index));

                var inputModel = await _categoryService.GetCategoryForDeletionAsync(id);

                if (inputModel == null)
                    return RedirectToAction(nameof(Index));

                return View(inputModel);  // <-- Return View here to show confirmation page
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (canDelete, blockingProducts) = await _categoryService.CanDeleteCategoryAsync(id);

            if (!canDelete)
            {
                TempData["DeleteError"] = $"Cannot delete category. It contains products: {string.Join(", ", blockingProducts)}.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            await _categoryService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
