using Azaliq.Data;
using Azaliq.Data.Models.Models.Enum;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.CartItems;
using Azaliq.ViewModels.Order;
using Azaliq.ViewModels.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Azaliq.Data.Models.Models;

namespace Azaliq.WebApp.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IPdfService _pdfService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public CartController(ApplicationDbContext dbContext, ICartService cartService, IOrderService orderService,
            IPdfService pdfService, UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _dbContext = dbContext;
            _cartService = cartService;
            _orderService = orderService;
            _pdfService = pdfService;
            _userManager = userManager;
            _emailService = emailService;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _cartService.AddToCartAsync(userId, productId, quantity);

            return RedirectToAction("Index", "Cart");
        }

        // POST: /Cart/Delete/{productId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _cartService.RemoveFromCartAsync(userId, productId);

            return RedirectToAction("Index", "Cart");
        }

        // POST: /Cart/UpdateAndCheckout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAndCheckout(List<CartItemViewModel> Items)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            var productIds = Items?.Select(i => i.ProductId).ToList() ?? new List<int>();
            var products = await _dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            bool hasQuantityIssue = Items.Any(item =>
                products.ContainsKey(item.ProductId) && item.Quantity > products[item.ProductId].Quantity);

            if (hasQuantityIssue)
            {
                ModelState.AddModelError(string.Empty, "You have entered too many.");

                var cartItems = await _cartService.GetCartItemsAsync(userId);

                var model = new CartIndexViewModel
                {
                    Items = cartItems ?? new List<CartItemViewModel>()
                };

                return View("Index", model);
            }

            foreach (var item in Items)
            {
                if (item.Quantity > 0)
                    await _cartService.UpdateQuantityAsync(userId, item.ProductId, item.Quantity);
                else
                    await _cartService.RemoveFromCartAsync(userId, item.ProductId);
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

            var stores = await _dbContext.StoresLocations
                .Where(s => !s.IsDeleted)
                .Select(s => new StoreDropDownModel
                {
                    Id = s.Id,
                    DisplayName = $"{s.Name} - {s.Address}"
                })
                .ToListAsync();

            // Optional: Prefill user info if you have it in your user profile
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var model = new CartCheckoutInfoInputViewModel
            {
                Items = cartItems,
                Email = user?.Email ?? "",
                Phone = user?.PhoneNumber ?? "",
                CountryCode = CountryCode.Bulgaria,
                Address = "",
                City = "",
                ZipCode = "",
                DeliveryOption = null,
                Stores = stores
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CartCheckoutInfoInputViewModel model)
        {
            // Reload stores if validation fails and view redisplayed
            model.Stores = await _dbContext.StoresLocations
                .Where(s => !s.IsDeleted)
                .Select(s => new StoreDropDownModel
                {
                    Id = s.Id,
                    DisplayName = $"{s.Name} - {s.Address}"
                })
                .ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "User must be logged in to place an order.");
                return View(model);
            }

            if (model.DeliveryOption == null)
            {
                ModelState.AddModelError(nameof(model.DeliveryOption), "Please select a delivery option.");
                return View(model);
            }

            if (!Enum.TryParse<DeliveryOptions>(model.DeliveryOption.ToString(), out var deliveryOption))
            {
                ModelState.AddModelError(nameof(model.DeliveryOption), "Invalid delivery option selected.");
                return View(model);
            }

            if (deliveryOption == DeliveryOptions.Courier && string.IsNullOrWhiteSpace(model.City))
            {
                ModelState.AddModelError(nameof(model.City), "City is required for courier delivery.");
                return View(model);
            }
            else if (deliveryOption == DeliveryOptions.PickupFromStore)
            {
                model.City = string.Empty;
            }

            var orderModel = new OrderDetailsViewModel
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                CountryCode = model.CountryCode,
                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode,
                DeliveryOption = deliveryOption.ToString(),
                PickupStoreId = deliveryOption == DeliveryOptions.PickupFromStore ? model.PickupStoreId : null
            };

            // Place order, get the saved order entity (make sure PlaceOrderAsync returns Order)
            var order = await _orderService.PlaceOrderAsync(orderModel, userId);

            if (order == null)
            {
                TempData["Error"] = "There was a problem placing your order.";
                return RedirectToAction("Index");
            }

            // Generate PDF bytes
            var pdfBytes = await _pdfService.GenerateOrderPdfWithQrAsync(order);

            // Send invoice PDF via email
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                await _emailService.SendEmailWithAttachmentAsync(
                    user.Email,
                    $"Your Azaliq Order Invoice - #{order.Id}",
                    "<p>Thank you for your order from Azaliq! Please find your invoice attached.</p>",
                    pdfBytes,
                    $"Azaliq_Invoice_{order.Id}.pdf"
                );
            }

            TempData["Success"] = "Order placed successfully! Invoice has been sent to your email.";
            return RedirectToAction("MyOrders", "Orders");
        }


    }
}