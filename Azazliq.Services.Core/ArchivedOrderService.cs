using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;

public class ArchivedUserService : IArchivedUserService
{
    private readonly ApplicationDbContext _context;

    public ArchivedUserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ArchivedUser>> GetAllArchivedUsersAsync()
    {
        return await _context.ArchivedUsers
            .Include(u => u.Orders)
            .ThenInclude(o => o.Products)
            .ToListAsync();
    }

    public async Task<ArchivedUser?> GetArchivedUserByIdAsync(Guid id)
    {
        return await _context.ArchivedUsers
            .Include(u => u.Orders)
            .ThenInclude(o => o.Products)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> ArchiveUserAsync(string userId)
    {
        var user = await _context.Set<ApplicationUser>()
            .Include(u => u.Orders)
                .ThenInclude(o => o.Products)
                    .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return false;

        var archivedUser = new ArchivedUser
        {
            Id = Guid.NewGuid(),
            OriginalUserId = user.Id,
            Email = user.Email ?? "",
            UserName = user.UserName ?? "",
            ArchivedOn = DateTime.UtcNow,
            Orders = new List<ArchivedOrder>()
        };

        _context.ArchivedUsers.Add(archivedUser);

        foreach (var order in user.Orders)
        {
            var totalAmount = order.Products.Sum(p => p.Product.Price * p.Quantity);

            var archivedOrder = new ArchivedOrder
            {
                Id = Guid.NewGuid(),
                ArchivedUserId = archivedUser.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = totalAmount,
                DeliveryAddress = order.DeliveryAddress ?? "",
                FullName = order.FullName ?? "",
                Email = order.Email ?? "",
                Phone = order.Phone ?? "",
                City = order.City ?? "",
                ZipCode = order.ZipCode ?? "",
                Products = new List<ArchivedOrderProduct>()
            };

            foreach (var product in order.Products)
            {
                var archivedProduct = new ArchivedOrderProduct
                {
                    Id = Guid.NewGuid(),
                    ArchivedOrderId = archivedOrder.Id,
                    ProductName = product.Product.Name,
                    Price = product.Product.Price,
                    Quantity = product.Quantity
                };

                archivedOrder.Products.Add(archivedProduct); // Add to collection
                _context.ArchivedOrderProducts.Add(archivedProduct); // Still tracked
            }

            archivedUser.Orders.Add(archivedOrder); // Add to collection
            _context.ArchivedOrders.Add(archivedOrder); // Still tracked
        }

        await _context.SaveChangesAsync();

        _context.OrdersProducts.RemoveRange(user.Orders.SelectMany(o => o.Products));
        _context.Orders.RemoveRange(user.Orders);
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }
}