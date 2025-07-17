using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Configurations;

namespace Azaliq.ViewModels.Product
{
    public class CreateProductInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        [Range(0.01, 100_000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10_000)]
        public int Quantity { get; set; }

        public bool IsSameDayDeliveryAvailable { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CreateProductDropDownCategory>? Categories { get; set; }


        public List<string>? SelectedTags { get; set; }

        public IEnumerable<string> AllTags { get; set; } 
            = new HashSet<string>();
    }
}
