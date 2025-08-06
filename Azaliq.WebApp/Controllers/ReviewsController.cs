using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azaliq.WebApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductReviews(int productId)
        {
            if (productId <= 0)
            {
                return RedirectToAction("Index", "Product");
            }

            var reviews = await _reviewService.GetReviewsForProductAsync(productId);

            var model = new ProductReviewsViewModel
            {
                ProductId = productId,
                Reviews = reviews.ToList()
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult AddReview(int productId)
        {
            var model = new ReviewInputModel { ProductId = productId };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ReviewInputModel model)
        {
            model.UserName = User.Identity?.Name ?? "Anonymous";
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _reviewService.AddReviewAsync(model);

            return RedirectToAction("Details", "Product", new { id = model.ProductId });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int productId)
        {
            bool deleted = await _reviewService.SoftDeleteReviewAsync(id);

            if (!deleted)
            {
                TempData["Error"] = "Review not found or already deleted.";
            }

            return RedirectToAction("ProductReviews", new { productId });
        }

    }
}
