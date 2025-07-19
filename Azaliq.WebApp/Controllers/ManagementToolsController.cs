using Azaliq.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class ManagementToolsController : BaseController
    {
        private readonly IUserRoleService _userRoleService;

        public ManagementToolsController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userRoleService
                .GetAllUsersWithManagerStatusAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToManager(string userId)
        {
            var success = await _userRoleService.PromoteToManagerAsync(userId);

            TempData[success ? "Success" : "Error"] = success
                ? "User promoted to Manager."
                : "User could not be promoted.";

            return RedirectToAction("UserList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveManagerRole(string userId)
        {
            var success = await _userRoleService.DemoteFromManagerAsync(userId);

            TempData[success ? "Success" : "Error"] = success
                ? "Manager role removed."
                : "Could not remove Manager role.";

            return RedirectToAction("UserList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var success = await _userRoleService.DeleteUserAsync(userId);

            TempData[success ? "Success" : "Error"] = success
                ? "User deleted successfully."
                : "User could not be deleted.";

            return RedirectToAction("UserList");
        }

    }
}
