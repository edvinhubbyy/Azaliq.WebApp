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

        // Create the archived user
        var archivedUser = new ArchivedUser
        {
            Id = Guid.NewGuid(),
            OriginalUserId = user.Id,
            Email = user.Email ?? "",
            UserName = user.UserName ?? "",
            Orders = new List<ArchivedOrder>()
        };

        _context.ArchivedUsers.Add(archivedUser);

        // Archive each order
        foreach (var order in user.Orders)
        {
            var archivedOrder = new ArchivedOrder
            {
                Id = Guid.NewGuid(),
                ArchivedUserId = archivedUser.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                FullName = order.FullName ?? "Unknown",
                Email = order.Email ?? "",
                Phone = order.Phone ?? "",
                City = order.City ?? "",
                ZipCode = order.ZipCode ?? "",
                DeliveryAddress = order.DeliveryAddress ?? "",
            };

            // Archive each product in the order
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

                archivedOrder.Products.Add(archivedProduct);

                // This line ensures the product is saved
                _context.ArchivedOrderProducts.Add(archivedProduct);
            }

            archivedUser.Orders.Add(archivedOrder);
            _context.ArchivedOrders.Add(archivedOrder); // Optional but good practice
        }

        // Save all archived data
        await _context.SaveChangesAsync();

        // Remove original user-related data
        var orderProductsToRemove = user.Orders.SelectMany(o => o.Products);
        _context.OrdersProducts.RemoveRange(orderProductsToRemove);
        _context.Orders.RemoveRange(user.Orders);
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }
}