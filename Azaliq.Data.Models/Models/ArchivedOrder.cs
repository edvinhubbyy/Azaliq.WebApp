using Azaliq.Data.Models.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("ArchivedOrder entity represents a snapshot of a customer's order when the user is deleted.")]
    public class ArchivedOrder
    {
        [Comment("Unique identifier for the ArchivedOrder.")]
        public Guid Id { get; set; }

        [Comment("Foreign key to the ArchivedUser who placed the ArchivedOrder.")]
        public Guid ArchivedUserId { get; set; }
        public ArchivedUser ArchivedUser { get; set; }

        [Comment("Date and time when the ArchivedOrder was placed.")]
        public DateTime OrderDate { get; set; }

        //[Comment("Optional date and time when the ArchivedOrder is scheduled for pickup.")]
        //public DateTime PickupTime { get; set; }

        [Comment("Status of the ArchivedOrder, indicating its current state in the order lifecycle.")]
        public OrderStatus Status { get; set; }

        [Comment("Collection of products associated with the ArchivedOrder.")]
        public ICollection<ArchivedOrderProduct> Products { get; set; } 
            = new HashSet<ArchivedOrderProduct>();

        [Comment("Total amount for the ArchivedOrder, calculated based on the products and their quantities.")]
        public decimal TotalAmount { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string DeliveryAddress { get; set; } = null!;

    }
}
