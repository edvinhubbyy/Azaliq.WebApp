using Azaliq.Data.Models.Models;
using Azaliq.ViewModels.Store;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Services.Core.Contracts
{
    public interface IStoreService
    {

        Task<IEnumerable<StoreLocationViewModel>> GetAllAsync();
        Task<StoreLocationViewModel?> GetByIdAsync(int id);
        Task<bool> AddressExistsAsync(string address, int? excludeId = null);
        Task AddAsync(CreateStoreLocationInputModel inputModel);
        Task<bool> UpdateAsync(EditStoreLocationInputModel inputModel);
        Task<bool> DeleteAsync(int id);

    }
}
