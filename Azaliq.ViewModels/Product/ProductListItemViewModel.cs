using System.ComponentModel.DataAnnotations;

namespace Azaliq.ViewModels.Product
{
    public class ProductListItemViewModel
    {
        public string? Id { get; set; }
        
        public string Name { get; set; } = null!;

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        
        public string Price { get; set; } = null!;

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }
    }
}
