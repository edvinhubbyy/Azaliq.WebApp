using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.CartItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Azaliq.WebApp.Controllers
{
    public class CartController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ApplicationDbContext dbContext, ICartService cartService, IOrderService orderService)
        {
            _dbContext = dbContext;
            _cartService = cartService;
            _orderService = orderService;
        }

        // GET: /Cart
        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = await _cartService.GetCartItemsAsync(userId);

            var model = new CartIndexViewModel
            {
                Items = cartItems ?? new List<CartItemViewModel>()
            };

            return View(model);
        }

        // POST: /Cart/Add/{productId}
        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCartAsync(userId, productId, quantity);

            return RedirectToAction("Index", "Cart");
        }

        // POST: /Cart/Delete/{productId}
        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.RemoveFromCartAsync(userId, productId);

            return RedirectToAction("Index", "Cart");
        }

        // POST: /Cart/Clear
        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.ClearCartAsync(userId);

            return RedirectToAction("Index", "Cart");
        }

        // POST: /Cart/UpdateQuantities
        [HttpPost]
        public async Task<IActionResult> UpdateQuantities(string action, Dictionary<int, int> quantities)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            if (action != null)
            {
                var parts = action.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int productId))
                {
                    if (parts[0] == "increase")
                    {
                        if (quantities.TryGetValue(productId, out int currentQty))
                        {
                            await _cartService.AddToCartAsync(userId, productId, 1);
                        }
                    }
                    else if (parts[0] == "decrease")
                    {
                        if (quantities.TryGetValue(productId, out int currentQty))
                        {
                            if (currentQty > 1)
                            {
                                await _cartService.UpdateQuantityAsync(userId, productId, currentQty - 1);
                            }
                            else
                            {
                                await _cartService.RemoveFromCartAsync(userId, productId);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // GET: /Cart/Checkout
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartService.GetCartItemsAsync(userId);

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var model = new CartIndexViewModel
            {
                Items = cartItems
            };

            return View(model);
        }

        // POST: /Cart/PlaceOrder
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string FullName, string Email, string Phone, bool isDelivery, string? deliveryAddress)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _orderService.PlaceOrderAsync(userId, isDelivery, deliveryAddress);

            TempData["SuccessMessage"] = "Order placed successfully!";
            return RedirectToAction("Index");
        }
    }
}
