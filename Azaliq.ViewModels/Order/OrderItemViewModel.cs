namespace Azaliq.ViewModels.Order
{
    public class OrderItemViewModel
    {
        public int OrderItemId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;
    }
}
