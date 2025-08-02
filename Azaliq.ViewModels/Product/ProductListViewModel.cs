using System.ComponentModel.DataAnnotations;

namespace Azaliq.ViewModels.Product
{
    public class ProductListViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category name")]
        public string CategoryName { get; set; } = null!;
        public IEnumerable<ProductListItemViewModel> Products { get; set; } = new List<ProductListItemViewModel>();
    }
}
