using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Data.Models.Models.Enum;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.CartItems;
using Azaliq.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Azaliq.WebApp.Controllers
{
    [Authorize]
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

        // POST: /Cart/UpdateAndCheckout
        [HttpPost]
        public async Task<IActionResult> UpdateAndCheckout(List<CartItemViewModel> Items)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            if (Items != null)
            {
                foreach (var item in Items)
                {
                    if (item.Quantity > 0)
                    {
                        await _cartService.UpdateQuantityAsync(userId, item.ProductId, item.Quantity);
                    }
                    else
                    {
                        await _cartService.RemoveFromCartAsync(userId, item.ProductId);
                    }
                }
            }

            return RedirectToAction("Checkout");
        }


        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartService.GetCartItemsAsync(userId);

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var model = new CartInputViewModel
            {
                Items = cartItems,
                FullName = "",
                Email = "",
                Phone = "",
                CountryCode = CountryCode.Bulgaria,
                Address = "",
                City = "",
                ZipCode = ""
            };

            return View(model);
        }

        // POST: /Cart/Checkout
        [HttpPost]
        public async Task<IActionResult> Checkout(CartInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // Handle error: user not logged in or user id missing
                ModelState.AddModelError("", "User must be logged in to place an order.");
                return View(model);
            }

            var orderModel = new OrderDetailsViewModel
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                CountryCode = model.CountryCode.ToString(),
                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode,
                // Items will be loaded inside PlaceOrderAsync from the cart by userId
            };

            await _orderService.PlaceOrderAsync(orderModel, userId);

            TempData["Success"] = "Order placed successfully!";
            return RedirectToAction("MyOrders", "Orders");
        }

    }

}

