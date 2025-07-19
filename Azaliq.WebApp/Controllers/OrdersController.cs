using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.CartItems;
using Azaliq.ViewModels.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azaliq.WebApp.Controllers
{

    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /Order/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Details(OrderDetailsViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model); // Show errors if form invalid
            }

            await _orderService.PlaceOrderAsync(model);

            TempData["Success"] = "Order placed successfully!";
            return RedirectToAction("MyOrders");
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

    }

}
