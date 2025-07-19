using Azaliq.ViewModels.Review;

namespace Azaliq.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string Price { get; set; } = null!;
        
        public int Quantity { get; set; }

        public string Category { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public bool IsSameDayDeliveryAvailable { get; set; }

        public ICollection<string> Tags { get; set; }
            = new HashSet<string>();

        public ICollection<ReviewViewModel> Reviews { get; set; } 
            = new HashSet<ReviewViewModel>();

    }
}
