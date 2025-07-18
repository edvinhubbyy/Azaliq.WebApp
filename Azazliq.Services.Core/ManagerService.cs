using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Services.Core
{
    public class ManagerService : IManagerService
    {

        private readonly ApplicationDbContext _dbContext;

        public ManagerService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Guid?> GetIdByUserIdAsync(string? userId)
        {
            Guid? managerId = null;

            if (!String.IsNullOrWhiteSpace(userId))
            {
                Manager? manager = await this._dbContext
                    .Managers
                    .FirstOrDefaultAsync(m => m.UserId.ToLower() == userId.ToLower());
                if (manager != null)
                {
                    managerId = manager.Id;
                }
            }

            return managerId;
        }

        public async Task<bool> ExistsByIdAsync(string? id)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(id))
            {
                result = await this._dbContext
                    .Managers
                    .AnyAsync(m => m.Id.ToString().ToLower() == id.ToLower());
            }
            return result;
        }

        public async Task<bool> ExistsByUserIdAsync(string? userId)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(userId))
            {
                result = await this._dbContext
                    .Managers
                    .AnyAsync(m => m.UserId.ToLower() == userId.ToLower());
            }
            return result;
        }
    }
}
