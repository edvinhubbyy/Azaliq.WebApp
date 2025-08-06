using Azaliq.ViewModels.Store;

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
