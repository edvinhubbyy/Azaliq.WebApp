using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class ProductController : BaseController
    {

        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        
        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();
            var products = await _productService.GetAllProductsAsync(userId);

            // TODO: Fix this
            return View(products);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                string? userId = GetUserId();
                ProductDetailsViewModel detailsVm =
                    await this._productService.GetProductDetailsAsync(id, userId);

                if (detailsVm == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return View(detailsVm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index), "Home");
            }

        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                CreateProductInputModel inputModel = new CreateProductInputModel
                {
                    Categories = await this._categoryService.GetCategoryDropDownDataAsync()
                };

                return this.View(inputModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    inputModel.Categories = await _categoryService.GetCategoryDropDownDataAsync();
                    return View(inputModel);
                }

                bool isCreated = await this._productService.CreateProductAsync(this.GetUserId()!, inputModel);

                if (!isCreated)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create product. Please try again.");
                    inputModel.Categories = await this._categoryService.GetCategoryDropDownDataAsync();
                    return this.View(inputModel);
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                string userId = GetUserId()!;

                var editProductInput = await _productService.EditProductAsync(userId, id);

                if (editProductInput == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                // Load categories dropdown into the view model
                editProductInput.Categories = await _categoryService.GetCategoryDropDownDataAsync();

                return View(editProductInput);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // If validation fails, reload categories dropdown to keep the view valid
                    inputModel.Categories = await _categoryService.GetCategoryDropDownDataAsync();
                    return View(inputModel);
                }

                bool editResult = await _productService.PersistUpdateProductAsync(GetUserId(), inputModel);

                if (!editResult)
                {
                    ModelState.AddModelError(string.Empty, "A fatal error occurred while updating the product.");
                    inputModel.Categories = await _categoryService.GetCategoryDropDownDataAsync();
                    return View(inputModel);
                }

                return RedirectToAction(nameof(Index), "Product");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Product/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var inputModel = await _productService.GetProductForDeletionAsync(id);

                if (inputModel == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(inputModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Optionally: log error and show friendly error page
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Product/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool isDeleted = await _productService.SoftDeleteProductAsync(id);

                if (!isDeleted)
                {
                    var inputModel = await _productService.GetProductForDeletionAsync(id);
                    ModelState.AddModelError(string.Empty, "Failed to delete product. Please try again.");
                    return View("Delete", inputModel);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Optionally: log error and show friendly error page
                return RedirectToAction(nameof(Index));
            }
        }





    }
}
