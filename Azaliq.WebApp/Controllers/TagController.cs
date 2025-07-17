using Azaliq.Data;
using Azaliq.Services.Core;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Category;
using Azaliq.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class TagController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ITagService _tagService;

        public TagController(ApplicationDbContext _dbContext, ICategoryService categoryService, IProductService productService, ITagService tagService)
        {
            this._dbContext = _dbContext;

            this._categoryService = categoryService;
            this._productService = productService;
            this._tagService = tagService;
        }

        [HttpGet]
        [AllowAnonymous] // Allow anonymous access to the index page
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

            bool tagExists = await _dbContext.ProductsTags
                .AnyAsync(t => t.Name.ToLower() == model.Name.ToLower());

            if (tagExists)
            {
                ModelState.AddModelError("Name", "Tag with this name already exists.");
                return View(model);
            }

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


        // GET: Tag/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var model = await _tagService.GetTagForDeletionAsync(id);

            if (model == null)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        // POST: Tag/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _tagService.DeleteTagAsync(id);

            if (!result)
            {
                // Could reload model and show error if desired
                ModelState.AddModelError(string.Empty, "Failed to delete tag.");
                var model = await _tagService.GetTagForDeletionAsync(id);
                return View("Delete", model);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
