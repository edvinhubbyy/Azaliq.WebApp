using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Configurations;
using Azaliq.Data.Models.Models.Enum;

namespace Azaliq.Data.Models.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public bool IsSameDayDeliveryAvailable { get; set; }

        public ICollection<ProductTag> Tags { get; set; }
            = new HashSet<ProductTag>();

        public bool IsDeleted { get; set; } = false;

    }
}
