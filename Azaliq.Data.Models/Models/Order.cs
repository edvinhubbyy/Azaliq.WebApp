using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Data.Models.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? PickupTime { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderProduct> Products { get; set; }
            = new HashSet<OrderProduct>();

        public decimal TotalAmount { get; set; }

        public bool IsDelivery { get; set; }

        public string? DeliveryAddress { get; set; }
    }

}
