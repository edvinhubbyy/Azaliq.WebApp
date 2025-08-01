﻿using Azaliq.Services.Core;
using Azaliq.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azaliq.WebApp.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IPdfService _pdfService;

        public OrdersController(IOrderService orderService, IPdfService pdfService)
        {
            _orderService = orderService;
            _pdfService = pdfService;
        }


        // GET: /Orders/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            // Return the order details view with OrderDetailsViewModel
            return View(order);
        }

        // GET: /Orders/MyOrders
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public async Task<IActionResult> DownloadInvoicePdf(int orderId)
        {
            var orderEntity = await _orderService.GetOrderEntityByIdAsync(orderId);
            if (orderEntity == null)
                return NotFound();

            var pdfBytes = await _pdfService.GenerateOrderPdfWithQrAsync(orderEntity);
            return File(pdfBytes, "application/pdf", $"Azaliq_Order_{orderEntity.Id}.pdf");
        }


        // POST: /Orders/Reorder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reorder(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _orderService.ReorderAsync(orderId, userId);

            if (!success)
            {
                TempData["Error"] = "Reorder failed. Product(s) may no longer be available.";
                return RedirectToAction("MyOrders");
            }

            TempData["Success"] = "Previous order added to your cart!";
            return RedirectToAction("Index", "Cart");
        }

        // GET: /Orders/History
        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);

            var completedOrders = orders
                .Where(o => o.Status == "Delivered" || o.Status == "Completed" || o.Status == "Cancelled")
                .ToList();

            return View(completedOrders);
        }
    }
}