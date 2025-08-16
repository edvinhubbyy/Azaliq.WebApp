using Azaliq.Data;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Archives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.WebApp.Controllers
{
    [Area("Admin")]
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
                .OrderByDescending(u => u.ArchivedOn)
                .Select(u => new ArchivedUserListItemViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    ArchivedOn = u.ArchivedOn
                })
                .ToListAsync();

            return View(archivedUsers);
        }

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

            var viewModel = new ArchivedUserDetailsViewModel
            {
                UserName = archivedUser.UserName,
                Email = archivedUser.Email,
                Orders = archivedUser.Orders.Select(o => new ArchivedOrderViewModel
                {
                    OrderDate = o.OrderDate,
                    Status = o.Status.ToString(),
                    TotalAmount = o.TotalAmount,
                    DeliveryAddress = o.DeliveryAddress,
                    Products = o.Products.Select(p => new ArchivedOrderProductViewModel
                    {
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Quantity = p.Quantity
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
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