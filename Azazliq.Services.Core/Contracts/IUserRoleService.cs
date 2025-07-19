using Azaliq.ViewModels.Admin;

namespace Azaliq.Services.Core.Contracts
{
    public interface IUserRoleService
    {
        Task<List<UserWithRoleViewModel>> GetAllUsersWithManagerStatusAsync();
        Task<bool> PromoteToManagerAsync(string userId);
        Task<bool> DemoteFromManagerAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);

    }

}
