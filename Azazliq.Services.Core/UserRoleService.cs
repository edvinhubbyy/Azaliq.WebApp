using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;

public class UserRoleService : IUserRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRoleService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    private const string AdminEmail = "admin@example.com";

    private bool IsAdminUser(ApplicationUser user)
        => user.Email != null && user.Email.Equals(AdminEmail, StringComparison.OrdinalIgnoreCase);

    public async Task<List<UserWithRoleViewModel>> GetAllUsersWithManagerStatusAsync()
    {
        var allUsers = _userManager.Users.ToList();

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
                IsManager = isManager
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

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}
