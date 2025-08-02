using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Product;

namespace Azaliq.ViewModels.Product
{
    public class ProductListItemViewModel
    {
        public string? Id { get; set; }
        
        public string Name { get; set; } = null!;

        [Display(Name = ImageUrlDisplay)]
        public string? ImageUrl { get; set; }
        
        public string Price { get; set; } = null!;

        [Display(Name = IsAvailableDisplay)]
        public bool IsAvailable { get; set; }
    }
}
