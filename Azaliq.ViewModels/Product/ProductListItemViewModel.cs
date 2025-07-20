namespace Azaliq.ViewModels.Product
{
    public class ProductListItemViewModel
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Price { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
