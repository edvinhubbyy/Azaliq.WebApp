using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("Review entity represents a product review in the system.")]
    public class Review
    {
        [Comment("Id of the Review")]
        public int Id { get; set; }

        [Comment("Id of the Product that is being reviewed")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Comment("Id of the User who wrote the review")]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [Comment("The content of the review")]
        public string? Comment { get; set; }

        [Comment("The rating given in the review, typically from 1 to 5 stars")]
        public int Rating { get; set; }

        [Comment("The date and time when the review was created")]
        public DateTime CreatedOn { get; set; }

        [Comment("Indicates whether the review has been deleted")]
        public bool IsDeleted { get; set; } = false;
    }

}
