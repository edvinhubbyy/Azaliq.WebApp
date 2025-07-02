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






    }
}
