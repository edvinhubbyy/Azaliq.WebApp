using Azaliq.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("Product entity represents a product in the system.")]
    public class Product
    {
        [Comment("Product ID")]
        public int Id { get; set; }

        [Comment("Product name")]
        public string Name { get; set; } = null!;

        [Comment("Product description")]
        public string? Description { get; set; }

        [Comment("Product image URL")]
        public string? ImageUrl { get; set; }

        [Comment("Product price")]
        public decimal Price { get; set; }

        [Comment("Product quantity in stock")]
        public int Quantity { get; set; }

        [Comment("Indicates if the product is available for purchase")]
        public bool IsAvailable { get; set; }

        [Comment("Category ID to which the product belongs")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [Comment("Indicates if the product is available for same-day delivery")]
        public bool IsSameDayDeliveryAvailable { get; set; }

        [Comment("Indicates if the product is available for next-day delivery")]
        public ICollection<ProductTag> Tags { get; set; }
            = new HashSet<ProductTag>();

        [Comment("Collection of reviews for the product")]
        public ICollection<Review> Reviews { get; set; } 
            = new HashSet<Review>();

        [Comment("Indicates if the product is deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
