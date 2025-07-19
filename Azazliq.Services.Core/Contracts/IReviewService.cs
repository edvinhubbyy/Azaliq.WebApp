using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
