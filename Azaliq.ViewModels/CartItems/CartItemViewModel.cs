namespace Azaliq.ViewModels.CartItems
{
    public class CartItemViewModel
    {
        public int Id { get; set; }  // CartItem ID, useful for unique identification
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductImageUrl { get; set; }  // Nullable, in case no image
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StockQuantity { get; set; }  // Represents the available stock for the product

        public decimal Subtotal => Price * Quantity;
    }
}
