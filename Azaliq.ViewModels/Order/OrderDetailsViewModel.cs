using Azaliq.Data.Models.Models.Enum;

namespace Azaliq.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
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
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public CountryCode CountryCode { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public List<OrderItemViewModel> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(item => item.Subtotal);
    }
}
