namespace Azaliq.ViewModels.CartItems
{
    public class CartDeleteViewModel
    {
        public int Id { get; set; } // CartItem ID
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
