using System.ComponentModel.DataAnnotations;

namespace Azaliq.ViewModels.Review
{
    public class ReviewInputModel
    {
        [Required]
        public int ProductId { get; set; }

        // No validation on UserId and UserName since set in controller
        public string? UserId { get; set; }
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}