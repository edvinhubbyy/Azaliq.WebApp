using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Review;

namespace Azaliq.ViewModels.Review
{
    public class ReviewInputModel
    {
        [Required]
        public int ProductId { get; set; }

        public string? UserId { get; set; }

        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = CommentRequired)]
        [StringLength(TextMaxLength, ErrorMessage = TextErrorMessage)]
        public string Comment { get; set; } = null!;

        [Required(ErrorMessage = RatingRequired)]
        [Range(RatingMinValue, RatingMaxValue, ErrorMessage = RatingRequiredErrorMessage)]
        public int Rating { get; set; }
    }
}