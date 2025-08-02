using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Product;

namespace Azaliq.ViewModels.Product
{
    public class ProductIndexViewModel
    {

        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        [Display(Name = SameDayDeliveryRequiredDisplay)]
        public bool IsSameDayDeliveryAvailable { get; set; }

    }
}
