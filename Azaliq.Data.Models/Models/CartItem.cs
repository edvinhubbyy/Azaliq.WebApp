using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("CartItem represents an item in a user's shopping cart, linking a product to a user with a specified quantity.")]
    public class CartItem
    {
        [Comment("Id is the unique identifier for the CartItem.")]
        public int Id { get; set; }

        [Comment("ProductId is the foreign key linking to the Product associated with this CartItem.")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Comment("UserId is the foreign key linking to the ApplicationUser who owns this CartItem.")]
        public ApplicationUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;

        [Comment("Quantity is the number of units of the product in the cart.")]
        public int Quantity { get; set; }
    }

}
