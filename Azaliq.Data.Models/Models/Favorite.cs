using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("Favorite entity represents a user's favorite product in the system.")]
    public class Favorite
    {
        [Comment("Favorite identifier")]
        public int Id { get; set; }

        [Comment("UserId is the identifier of the user who favorited the product.")]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [Comment("ProductId is the identifier of the product that has been favorited.")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
