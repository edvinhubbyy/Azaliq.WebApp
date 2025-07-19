using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Review;
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

        // GET: Reviews/ProductReviews?productId=123
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

        // GET: Reviews/Add?productId=123
        [HttpGet]
        public IActionResult AddReview(int productId)
        {
            var model = new ReviewInputModel { ProductId = productId };
            return View(model);
        }


        // POST: Reviews/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ReviewInputModel model)
        {
            // Assign UserName and UserId before model validation
            model.UserName = User.Identity?.Name ?? "Anonymous";
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _reviewService.AddReviewAsync(model);

            return RedirectToAction("Details", "Product", new { id = model.ProductId });
        }


        // POST: Reviews/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int productId)
        {
            bool deleted = await _reviewService.SoftDeleteReviewAsync(id);

            if (!deleted)
            {
                TempData["Error"] = "Review not found or already deleted.";
            }

            return RedirectToAction("Details", "Product", new { id = productId });
        }
    }
}
