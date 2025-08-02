using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.ViewModels.Product
{
    public class EditProductInputModel
    {

        [Required]
        public int Id { get; set; } // Needed to identify which product to update

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Url]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Range(0.01, 100_000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10_000)]
        public int Quantity { get; set; }

        [Display(Name = "Is same day delivery available")]
        public bool IsSameDayDeliveryAvailable { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CreateProductDropDownCategory>? Categories { get; set; }

        [Display(Name = "Tags")]
        public List<string>? SelectedTags { get; set; }

        [Display(Name = "All tags")]
        public IEnumerable<string> AllTags { get; set; } 
            = new HashSet<string>();
    }
}
