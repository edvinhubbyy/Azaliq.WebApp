namespace Azaliq.ViewModels.Order
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public List<OrderItemViewModel> Items { get; set; } = new();

        // Calculate total on the fly
        public decimal TotalAmount => Items.Sum(i => i.Subtotal);
    }

}
