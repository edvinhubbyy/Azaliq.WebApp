using Azaliq.Data;
using Azaliq.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.WebApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IArchivedUserService _archivedUserService;

        public UserManagementController(ApplicationDbContext context, IArchivedUserService archivedUserService)
        {
            _context = context;
            _archivedUserService = archivedUserService;
        }

        // GET: /UserManagement/Index
        public async Task<IActionResult> Index()
        {
            var archivedUsers = await _context.ArchivedUsers
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Products)
                .ToListAsync();

            return View(archivedUsers);
        }

        // GET: /UserManagement/ArchivedUserDetails/{id}
        public async Task<IActionResult> ArchivedUserDetails(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
                return NotFound();

            var archivedUser = await _context.ArchivedUsers
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Products)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (archivedUser == null)
                return NotFound();

            return View(archivedUser);
        }

        // POST: /UserManagement/ArchiveUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var success = await _archivedUserService.ArchiveUserAsync(userId);

            if (success)
                TempData["Success"] = "User archived successfully.";
            else
                TempData["Error"] = "User not found or could not be archived.";

            return RedirectToAction(nameof(Index));
        }
    }
}