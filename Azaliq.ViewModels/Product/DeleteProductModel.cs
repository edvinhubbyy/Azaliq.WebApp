namespace Azaliq.ViewModels.Product
{
    public class DeleteProductModel
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsSameDayDeliveryAvailable { get; set; }

        public int CategoryId { get; set; }

        public bool IsDeleted { get; set; }



    }
}