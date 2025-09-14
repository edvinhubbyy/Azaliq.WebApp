using Azaliq.Data;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Services.Core
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LowStockProductViewModel>> GetLowStockProductsAsync(int threshold = 10)
        {
            return await _context.Products
                .Where(p => !p.IsDeleted && p.Quantity <= threshold)
                .Include(p => p.Category)
                .Select(p => new LowStockProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    CurrentStock = p.Quantity,
                    CategoryName = p.Category.Name,
                    IsAvailable = p.IsAvailable
                })
                .OrderBy(p => p.CurrentStock)
                .ToListAsync();
        }

        public async Task<InventoryDashboardViewModel> GetInventoryDashboardAsync()
        {
            var products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .ToListAsync();

            var dashboard = new InventoryDashboardViewModel
            {
                TotalProducts = products.Count,
                OutOfStockCount = products.Count(p => p.Quantity == 0),
                CriticalStockCount = products.Count(p => p.Quantity > 0 && p.Quantity <= 5),
                LowStockCount = products.Count(p => p.Quantity > 5 && p.Quantity <= 10),
                TotalInventoryValue = products.Sum(p => p.Price * p.Quantity)
            };

            dashboard.LowStockProducts = products
                .Where(p => p.Quantity <= 10)
                .Select(p => new LowStockProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    CurrentStock = p.Quantity,
                    CategoryName = p.Category.Name,
                    IsAvailable = p.IsAvailable
                })
                .OrderBy(p => p.CurrentStock)
                .Take(10)
                .ToList();

            dashboard.CategorySummaries = products
                .GroupBy(p => p.Category.Name)
                .Select(g => new CategoryStockSummary
                {
                    CategoryName = g.Key,
                    TotalProducts = g.Count(),
                    OutOfStockProducts = g.Count(p => p.Quantity == 0),
                    LowStockProducts = g.Count(p => p.Quantity > 0 && p.Quantity <= 10),
                    TotalValue = g.Sum(p => p.Price * p.Quantity)
                })
                .OrderByDescending(c => c.TotalValue)
                .ToList();

            return dashboard;
        }

        public async Task<bool> UpdateStockAsync(int productId, int newQuantity, string updatedBy)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || product.IsDeleted)
                return false;

            var previousQuantity = product.Quantity;
            product.Quantity = newQuantity;

            // Update availability based on stock
            product.IsAvailable = newQuantity > 0;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AdjustStockAsync(int productId, int adjustment, string adjustedBy, string reason)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || product.IsDeleted)
                return false;

            var previousQuantity = product.Quantity;
            var newQuantity = Math.Max(0, previousQuantity + adjustment);
            
            product.Quantity = newQuantity;
            product.IsAvailable = newQuantity > 0;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<StockMovementViewModel>> GetStockMovementHistoryAsync(int productId)
        {
            // This would typically come from a stock movement log table
            // For now, we'll return an empty list as we haven't implemented stock movement logging yet
            // This is a feature that could be added in the future
            return new List<StockMovementViewModel>();
        }
    }
}