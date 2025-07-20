using Azaliq.Data.Models.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("Order entity represents a customer's order in the system.")]
    public class Order
    {
        [Comment("Unique identifier for the Order.")]
        public int Id { get; set; }

        [Comment("Foreign key to the User who placed the Order.")]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [Comment("Date and time when the Order was placed.")]
        public DateTime OrderDate { get; set; }
        
        [Comment("Optional date and time when the Order is scheduled for pickup.")]
        public DateTime? PickupTime { get; set; }

        [Comment("Status of the Order, indicating its current state in the order lifecycle.")]
        public OrderStatus Status { get; set; }

        [Comment("Collection of products associated with the Order.")]
        public ICollection<OrderProduct> Products { get; set; }
            = new HashSet<OrderProduct>();

        [Comment("Total amount for the Order, calculated based on the products and their quantities.")]
        public decimal TotalAmount { get; set; }

        [Comment("Indicates whether the Order is for delivery or pickup.")]
        public bool IsDelivery { get; set; }

        [Comment("Optional delivery address for the Order, if it is a delivery order.")]
        public string? DeliveryAddress { get; set; }

        [Comment("Indicates whether the Order has been deleted or is active.")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Added customer/order details:")]
        // Added customer/order details:
        public string FullName { get; set; } = null!;
        
        public string Email { get; set; } = null!;
        
        public string Phone { get; set; } = null!;
        
        public CountryCode CountryCode { get; set; }
        
        public string City { get; set; } = null!;
        
        public string ZipCode { get; set; } = null!;


    }

}
