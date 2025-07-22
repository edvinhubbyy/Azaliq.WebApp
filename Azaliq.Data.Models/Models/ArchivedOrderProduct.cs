namespace Azaliq.Data.Models.Models
{
    public class ArchivedOrderProduct
    {
        public Guid Id { get; set; }

        public Guid ArchivedOrderId { get; set; }
        public ArchivedOrder ArchivedOrder { get; set; } = null!;

        // Copy necessary product details here
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
