namespace Azaliq.Data.Models.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public int Quantity { get; set; }
    }

}
