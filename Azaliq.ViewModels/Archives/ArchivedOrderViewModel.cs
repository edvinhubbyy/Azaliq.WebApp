namespace Azaliq.ViewModels.Archives
{
    public class ArchivedOrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string? DeliveryAddress { get; set; }
        public IEnumerable<ArchivedOrderProductViewModel> Products { get; set; } = new List<ArchivedOrderProductViewModel>();
    }
}
