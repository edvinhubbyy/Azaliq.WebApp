namespace Azaliq.ViewModels.Order
{
    public class DeleteOrderModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        
        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}
