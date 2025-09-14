using Azaliq.ViewModels.Inventory;

namespace Azaliq.Services.Core.Contracts
{
    public interface IInventoryService
    {
        Task<List<LowStockProductViewModel>> GetLowStockProductsAsync(int threshold = 10);
        Task<InventoryDashboardViewModel> GetInventoryDashboardAsync();
        Task<bool> UpdateStockAsync(int productId, int newQuantity, string updatedBy);
        Task<bool> AdjustStockAsync(int productId, int adjustment, string adjustedBy, string reason);
        Task<List<StockMovementViewModel>> GetStockMovementHistoryAsync(int productId);
    }
}