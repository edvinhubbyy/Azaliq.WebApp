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

        public CartController(ApplicationDbContext _dbContext, ICartService cartService)
        {
            this._dbContext = _dbContext;

            this._cartService = cartService;
        }

        // GET: /Cart
        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = await _cartService.GetCartItemsAsync(userId);

            // if GetCartItemsAsync returns List<CartItemViewModel>, wrap it:
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
                        // Increase quantity by 1
                        if (quantities.TryGetValue(productId, out int currentQty))
                        {
                            await _cartService.AddToCartAsync(userId, productId, 1);
                        }
                    }
                    else if (parts[0] == "decrease")
                    {
                        // Decrease quantity by 1, or remove if 1
                        if (quantities.TryGetValue(productId, out int currentQty))
                        {
                            if (currentQty > 1)
                            {
                                // Update to new quantity (currentQty - 1)
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

    }
}
