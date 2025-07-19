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

        //public IActionResult UserList()
        //{
        //    var users = _userManager.Users.ToList();
        //    return View(users);
        //}

        //[HttpPost]
        //public async Task<IActionResult> PromoteToManager(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        TempData["Error"] = "User not found.";
        //        return RedirectToAction("UserList");
        //    }

        //    var isAlreadyManager = await _userManager.IsInRoleAsync(user, "Manager");

        //    if (!isAlreadyManager)
        //    {
        //        await _userManager.AddToRoleAsync(user, "Manager");
        //        TempData["Success"] = "User promoted to Manager.";
        //    }
        //    else
        //    {
        //        TempData["Info"] = "User is already a Manager.";
        //    }

        //    return RedirectToAction("UserList");
        //}
        
        //[HttpPost]
        //public async Task<IActionResult> RemoveManagerRole(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        TempData["Error"] = "User not found.";
        //        return RedirectToAction("UserList");
        //    }

        //    var isManager = await _userManager.IsInRoleAsync(user, "Manager");

        //    if (isManager)
        //    {
        //        await _userManager.RemoveFromRoleAsync(user, "Manager");
        //        TempData["Success"] = "Manager role removed.";
        //    }
        //    else
        //    {
        //        TempData["Info"] = "User is not a Manager.";
        //    }

        //    return RedirectToAction("UserList");
        //}
    }
}
