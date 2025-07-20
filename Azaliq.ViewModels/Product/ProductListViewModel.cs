namespace Azaliq.ViewModels.Product
{
    public class ProductListViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public IEnumerable<ProductListItemViewModel> Products { get; set; } = new List<ProductListItemViewModel>();
    }
}
