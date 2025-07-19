namespace Azaliq.ViewModels.Review
{
    public class ProductReviewsViewModel
    {
        public int ProductId { get; set; }
        
        public ICollection<ReviewViewModel> Reviews { get; set; } =
            new HashSet<ReviewViewModel>();
    }
}
