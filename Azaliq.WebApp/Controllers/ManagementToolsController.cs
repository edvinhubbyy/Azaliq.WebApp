using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    [Area("Admin")]
    public class ManagementToolsController : BaseController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ApplicationDbContext _context;

        public ManagementToolsController(IUserRoleService userRoleService, ApplicationDbContext _context)
        {
            _userRoleService = userRoleService;
            this._context = _context;
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userRoleService
                .GetAllUsersWithManagerStatusAsync();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteToManager(string userId)
        {
            var success = await _userRoleService.PromoteToManagerAsync(userId);

            TempData[success ? "Success" : "Error"] = success
                ? "User promoted to Manager."
                : "User could not be promoted.";

            return RedirectToAction(nameof(UserList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveManagerRole(string userId)
        {
            var success = await _userRoleService.DemoteFromManagerAsync(userId);

            TempData[success ? "Success" : "Error"] = success
                ? "Manager role removed."
                : "Could not remove Manager role.";

            return RedirectToAction(nameof(UserList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var success = await _userRoleService.DeleteUserAsync(userId);

            TempData[success ? "Success" : "Error"] = success
                ? "User deleted successfully."
                : "User could not be deleted.";

            return RedirectToAction(nameof(UserList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var user = await _context
                .Set<ApplicationUser>()
                .FindAsync(userId);

            if (user == null)
                return NotFound();

            user.IsBanned = true;

            await _context.SaveChangesAsync();

            TempData["Success"] = $"User {user.UserName} has been banned.";

            return RedirectToAction(nameof(UserList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnbanUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var user = await _context
                .Set<ApplicationUser>()
                .FindAsync(userId);

            if (user == null)
                return NotFound();

            user.IsBanned = false;

            await _context.SaveChangesAsync();

            TempData["Success"] = $"User {user.UserName} has been unbanned.";

            return RedirectToAction(nameof(UserList));
        }

    }
}
