using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Configurations
{
    [Comment("Category entity represents a product category in the system.")]
    public class Category
    {
        [Comment("Unique identifier for the Category.")]
        public int Id { get; set; }

        [Comment("Name of the Category.")]
        public string Name { get; set; } = null!;

        [Comment("Description of the Category.")]
        public ICollection<Product> Products { get; set; }
            = new HashSet<Product>();

        [Comment("Filtering property")]
        public bool IsDeleted { get; set; } = false;

    }
}
