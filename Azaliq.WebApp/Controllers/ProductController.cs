using Azaliq.Data;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ProductController : BaseController
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(ApplicationDbContext _dbContext, ICategoryService categoryService, IProductService productService)
        {
            this._dbContext = _dbContext;

            this._categoryService = categoryService;
            this._productService = productService;
        }

        [HttpGet]
        [AllowAnonymous] // Allow anonymous access to the index page
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();
            var products = await _productService.GetAllProductsAsync(userId);

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
                    Categories = await this._categoryService.GetCategoryDropDownDataAsync(),
                    AllTags = await _dbContext.ProductsTags.Select(t => t.Name).ToListAsync(),
                    SelectedTags = new List<string>()
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
        public async Task<IActionResult> Create(CreateProductInputModel inputModel, string? NewTags)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    inputModel.Categories = await _categoryService.GetCategoryDropDownDataAsync();
                    return View(inputModel);
                }

                // Parse new tags (comma-separated), trim, and merge with selected tags
                var newTagsList = new List<string>();
                if (!string.IsNullOrWhiteSpace(NewTags))
                {
                    newTagsList = NewTags
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim())
                        .Where(t => !string.IsNullOrWhiteSpace(t))
                        .ToList();
                }

                inputModel.SelectedTags = inputModel.SelectedTags ?? new List<string>();
                inputModel.SelectedTags.AddRange(newTagsList);

                // Remove duplicates ignoring case
                inputModel.SelectedTags = inputModel.SelectedTags
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

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
            if (id == null)
                return RedirectToAction(nameof(Index));

            var userId = GetUserId()!;

            var editModel = await _productService.EditProductAsync(userId, id);

            if (editModel == null)
                return RedirectToAction(nameof(Index));

            // Populate categories dropdown
            editModel.Categories = await _categoryService.GetCategoryDropDownDataAsync();

            // SelectedTags should already be populated in EditProductAsync

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductInputModel inputModel, string? newTags)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    inputModel.Categories = await _categoryService.GetCategoryDropDownDataAsync();
                    return View(inputModel);
                }

                // Merge newTags (comma separated string) with SelectedTags
                if (!string.IsNullOrWhiteSpace(newTags))
                {
                    var newTagsList = newTags
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .ToList();

                    inputModel.SelectedTags ??= new List<string>();

                    // Add new tags if not already present
                    foreach (var tag in newTagsList)
                    {
                        if (!inputModel.SelectedTags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                        {
                            inputModel.SelectedTags.Add(tag);
                        }
                    }
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
