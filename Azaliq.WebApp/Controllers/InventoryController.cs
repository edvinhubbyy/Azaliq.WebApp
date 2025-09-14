using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class InventoryController : BaseController
    {
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;

        public InventoryController(IInventoryService inventoryService, IProductService productService)
        {
            _inventoryService = inventoryService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await _inventoryService.GetInventoryDashboardAsync();
            return View(dashboard);
        }

        [HttpGet]
        public async Task<IActionResult> LowStock(int threshold = 10)
        {
            var lowStockProducts = await _inventoryService.GetLowStockProductsAsync(threshold);
            ViewBag.Threshold = threshold;
            return View(lowStockProducts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStock(int productId, int newQuantity)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return BadRequest("User not found");
            }

            var success = await _inventoryService.UpdateStockAsync(productId, newQuantity, userId);
            
            if (success)
            {
                TempData["SuccessMessage"] = "Stock updated successfully!";
                return Json(new { success = true, message = "Stock updated successfully!" });
            }
            
            return Json(new { success = false, message = "Failed to update stock." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdjustStock([FromBody] StockAdjustmentInputModel model)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return BadRequest("User not found");
            }

            var success = await _inventoryService.AdjustStockAsync(
                model.ProductId, 
                model.AdjustmentAmount, 
                userId, 
                model.Reason ?? "Manual adjustment");
            
            if (success)
            {
                return Json(new { success = true, message = "Stock adjusted successfully!" });
            }
            
            return Json(new { success = false, message = "Failed to adjust stock." });
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var dashboard = await _inventoryService.GetInventoryDashboardAsync();
            return Json(dashboard);
        }
    }
}