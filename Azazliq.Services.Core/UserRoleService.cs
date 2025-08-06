using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRoleService : IUserRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context; // Inject your DbContext

    public UserRoleService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    private const string AdminEmail = "admin@example.com";

    private bool IsAdminUser(ApplicationUser user)
        => user.Email != null && user.Email.Equals(AdminEmail, StringComparison.OrdinalIgnoreCase);

    public async Task<List<UserWithRoleViewModel>> GetAllUsersWithManagerStatusAsync()
    {
        var allUsers = await _userManager.Users
            .ToListAsync();

        var filteredUsers = allUsers
            .Where(u => !IsAdminUser(u))
            .ToList();

        var result = new List<UserWithRoleViewModel>();

        foreach (var user in filteredUsers)
        {
            var isManager = await _userManager.IsInRoleAsync(user, "Manager");

            result.Add(new UserWithRoleViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                IsManager = isManager,
                IsBanned = user.IsBanned
            });
        }

        return result;
    }


    public async Task<bool> PromoteToManagerAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || IsAdminUser(user) || await _userManager.IsInRoleAsync(user, "Manager"))
            return false;

        var result = await _userManager.AddToRoleAsync(user, "Manager");

        return result.Succeeded;
    }

    public async Task<bool> DemoteFromManagerAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || IsAdminUser(user) || !(await _userManager.IsInRoleAsync(user, "Manager")))
            return false;

        var result = await _userManager.RemoveFromRoleAsync(user, "Manager");

        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || IsAdminUser(user))
            return false;
        var orders = await _context.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync();
        var orderIds = orders.Select(o => o.Id).ToList();
        var orderProducts = await _context.OrdersProducts
            .Where(op => orderIds.Contains(op.OrderId))
            .ToListAsync();

        _context.OrdersProducts.RemoveRange(orderProducts);
        _context.Orders.RemoveRange(orders);

        await _context.SaveChangesAsync();
        var result = await _userManager.DeleteAsync(user);

        return result.Succeeded;
    }


}