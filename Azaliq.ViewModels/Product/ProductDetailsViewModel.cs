using System.ComponentModel.DataAnnotations;
using Azaliq.ViewModels.Review;

namespace Azaliq.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        public string Price { get; set; } = null!;
        
        public int Quantity { get; set; }

        public string Category { get; set; } = null!;

        [Display(Name = "Is available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Is same day delivery available")]
        public bool IsSameDayDeliveryAvailable { get; set; }

        public ICollection<string> Tags { get; set; }
            = new HashSet<string>();

        public ICollection<ReviewViewModel> Reviews { get; set; } 
            = new HashSet<ReviewViewModel>();

    }
}
