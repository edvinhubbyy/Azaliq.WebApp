using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Services.Core
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReviewService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsForProductAsync(int productId)
        {
            return await _dbContext.Reviews
                .Where(r => r.ProductId == productId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    UserName = r.User.UserName,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    CreatedOn = r.CreatedOn
                })
                .ToListAsync();

        }

        public async Task AddReviewAsync(ReviewInputModel model)
        {
            var review = new Review
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                Comment = model.Comment,
                Rating = model.Rating,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> SoftDeleteReviewAsync(int reviewId)
        {
            var review = await _dbContext.Reviews.FindAsync(reviewId);
            if (review == null || review.IsDeleted)
                return false;

            review.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
