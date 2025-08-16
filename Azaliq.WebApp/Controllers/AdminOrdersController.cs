using Azaliq.Data.Models.Models;
using Azaliq.Services.Core;
using Azaliq.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class AdminOrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdminOrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        // GET: /AdminOrders/All
        public async Task<IActionResult> All()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // POST: /AdminOrders/ChangeStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int orderId, string newStatus)
        {
            var success = await _orderService.ChangeStatusAsync(orderId, newStatus);
            if (!success)
            {
                TempData["Error"] = "Could not update order status.";
            }
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)  // parameter name should match route param
        {
            var model = await _orderService.GetOrderByIdForDeleteAsync(id);
            if (model == null)
                return RedirectToAction("All");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int orderId)
        {
            var success = await _orderService.SoftDeleteOrderAsync(orderId);
            if (!success)
            {
                TempData["DeleteError"] = "Order could not be deleted.";
                var model = await _orderService.GetOrderByIdForDeleteAsync(orderId);
                return View("Delete", model);
            }

            TempData["SuccessMessage"] = "Order deleted successfully.";
            return RedirectToAction("All");
        }

    }
}
