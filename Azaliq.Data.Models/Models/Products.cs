using Azaliq.Data.Configurations;

namespace Azaliq.Data.Models.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public bool IsSameDayDeliveryAvailable { get; set; }

        public ICollection<ProductTag> Tags { get; set; }
            = new HashSet<ProductTag>();

        public ICollection<Review> Reviews { get; set; } 
            = new HashSet<Review>();


        public bool IsDeleted { get; set; } = false;

    }
}
