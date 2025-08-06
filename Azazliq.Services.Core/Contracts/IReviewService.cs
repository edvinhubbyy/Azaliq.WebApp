using Azaliq.ViewModels.Review;

namespace Azaliq.Services.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetReviewsForProductAsync(int productId);
        Task AddReviewAsync(ReviewInputModel input);
        Task<bool> SoftDeleteReviewAsync(int reviewId);
    }
}
