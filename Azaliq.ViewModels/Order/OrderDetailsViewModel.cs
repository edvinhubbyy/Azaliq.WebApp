using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Models.Models.Enum;
using Azaliq.Data.Models.Models.Enum.Phone;

namespace Azaliq.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        // Order info
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        
        public string DeliveryOption { get; set; } = null!;
        
        public bool IsDelivery { get; set; }
        public string? DeliveryAddress { get; set; }

        public int? PickupStoreId { get; set; }
        public string? PickupStoreUrl { get; set; }




        // Customer info
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public CountryCode CountryCode { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        
        
        // Order items
        public List<OrderItemViewModel> Items { get; set; } = new();

        // Convenience property for sum of all items subtotals
        public decimal TotalPrice => Items.Sum(item => item.Subtotal);
        
    }
}
