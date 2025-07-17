using Azaliq.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class AdminOrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public AdminOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /AdminOrders/All
        public async Task<IActionResult> All()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // POST: /AdminOrders/ChangeStatus
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int orderId, string newStatus)
        {
            var success = await _orderService.ChangeStatusAsync(orderId, newStatus);
            if (!success)
            {
                TempData["Error"] = "Could not update order status.";
            }
            return RedirectToAction(nameof(All));
        }

        // POST: /AdminOrders/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int orderId)
        {
            var success = await _orderService.DeleteOrderAsync(orderId);
            if (!success)
            {
                TempData["Error"] = "Could not delete order.";
            }
            return RedirectToAction(nameof(All));
        }
    }
}
