using Azaliq.Data.Models.Models;

namespace Azaliq.Services.Core.Contracts
{
    public interface IArchivedUserService
    {
        Task<IEnumerable<ArchivedUser>> GetAllArchivedUsersAsync();
        Task<ArchivedUser?> GetArchivedUserByIdAsync(Guid id);
        Task<bool> ArchiveUserAsync(string userId);
    }
}
