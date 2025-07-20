using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("ProductTags")]
    public class ProductTag
    {
        [Comment("ID of the ProductTag")]
        public int Id { get; set; }

        [Comment("Name of the ProductTag")]
        public string Name { get; set; } = null!;

        [Comment("Products associated with this tag")]
        public ICollection<Product> Products { get; set; }
            = new HashSet<Product>();
    }

}
