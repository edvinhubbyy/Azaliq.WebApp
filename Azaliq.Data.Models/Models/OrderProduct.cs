using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("OrderProduct a join entity that represents the many-to-many relationship between Order and Product.")]
    public class OrderProduct
    {

        [Comment("Primary key for the OrderProduct entity.")]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [Comment("Primary key for the OrderProduct entity.")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Comment("The quantity of the product in the order.")]
        public int Quantity { get; set; }
    }

}