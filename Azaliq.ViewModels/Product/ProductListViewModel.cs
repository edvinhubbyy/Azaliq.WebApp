using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Product;

namespace Azaliq.ViewModels.Product
{
    public class ProductListViewModel
    {
        public int CategoryId { get; set; }
        
        [Display(Name = CategoryNameDisplay)]
        public string CategoryName { get; set; } = null!;
        
        public IEnumerable<ProductListItemViewModel> Products { get; set; } = new List<ProductListItemViewModel>();
    }
}
