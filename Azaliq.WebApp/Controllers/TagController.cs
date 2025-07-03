using Azaliq.Services.Core;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Category;
using Azaliq.ViewModels.Tag;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class TagController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ITagService _tagService;

        public TagController(ICategoryService categoryService, IProductService productService, ITagService tagService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
            this._tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetAllTagsAsync(); // or pass userId if needed
            return View(tags);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateTagInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _tagService.AddTagAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {

                TagEditInputModel? editTag
                    = await this._tagService
                        .GetTagByIdAsync(id);

                if (editTag == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return View(editTag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagEditInputModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(inputModel);
                }

                bool editResult = await this._tagService
                    .UpdateTagAsync(inputModel);

                if (editResult == false)
                {
                    this.ModelState.AddModelError(string.Empty, "Fatal error occured while updating the tag");
                    return this.View(inputModel);
                }

                return this.RedirectToAction(nameof(Index), "Tag");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index));

            }
        }
    }
}
