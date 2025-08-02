using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Display(Name = "Is same day delivery available")]
        public bool IsSameDayDeliveryAvailable { get; set; }

    }
}
