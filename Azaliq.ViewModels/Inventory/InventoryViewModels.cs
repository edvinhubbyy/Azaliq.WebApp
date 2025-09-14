namespace Azaliq.ViewModels.Inventory
{
    public class LowStockProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int CurrentStock { get; set; }
        public int RecommendedStock { get; set; } = 20;
        public string CategoryName { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public string StockStatus => CurrentStock switch
        {
            0 => "Out of Stock",
            <= 5 => "Critical",
            <= 10 => "Low",
            _ => "Normal"
        };
    }

    public class InventoryDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int OutOfStockCount { get; set; }
        public int LowStockCount { get; set; }
        public int CriticalStockCount { get; set; }
        public decimal TotalInventoryValue { get; set; }
        public List<LowStockProductViewModel> LowStockProducts { get; set; } = new();
        public List<CategoryStockSummary> CategorySummaries { get; set; } = new();
    }

    public class CategoryStockSummary
    {
        public string CategoryName { get; set; } = null!;
        public int TotalProducts { get; set; }
        public int OutOfStockProducts { get; set; }
        public int LowStockProducts { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class StockMovementViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int PreviousQuantity { get; set; }
        public int NewQuantity { get; set; }
        public int AdjustmentAmount { get; set; }
        public string MovementType { get; set; } = null!; // Sale, Restock, Adjustment, Return
        public string? Reason { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
    }

    public class StockAdjustmentInputModel
    {
        public int ProductId { get; set; }
        public int AdjustmentAmount { get; set; }
        public string? Reason { get; set; }
    }
}