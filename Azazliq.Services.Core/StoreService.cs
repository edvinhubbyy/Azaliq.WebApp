using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Store;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Services.Core
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoreLocationViewModel>> GetAllAsync()
        {
            return await _context.StoresLocations
                .Select(s => new StoreLocationViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    GoogleMapsUrl = s.GoogleMapsUrl,
                    Address = s.Address,
                    Phone = s.PhoneNumber,
                    CountryCode = s.CountryCode
                })
                .ToListAsync();
        }

        public async Task<StoreLocationViewModel?> GetByIdAsync(int id)
        {
            var store = await _context.StoresLocations.FindAsync(id);

            if (store == null) return null;

            return new StoreLocationViewModel
            {
                Id = store.Id,
                Name = store.Name,
                GoogleMapsUrl = store.GoogleMapsUrl,
                Address = store.Address,
                Phone = store.PhoneNumber,
                CountryCode = store.CountryCode
            };
        }

        // Checks if the address exists, excluding the store with excludeId if provided
        public async Task<bool> AddressExistsAsync(string address, int? excludeId = null)
        {
            var query = _context.StoresLocations.AsQueryable();

            if (excludeId.HasValue)
            {
                query = query.Where(s => s.Id != excludeId.Value);
            }

            return await query.AnyAsync(s => s.Address.ToLower() == address.ToLower());
        }

        public async Task AddAsync(CreateStoreLocationInputModel inputModel)
        {
            var store = new Store
            {
                Name = inputModel.Name,
                GoogleMapsUrl = inputModel.GoogleMapsUrl,
                Address = inputModel.Address,
                PhoneNumber = inputModel.PhoneNumber,
                CountryCode = inputModel.CountryCode
            };

            _context.StoresLocations.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(EditStoreLocationInputModel inputModel)
        {
            var store = await _context.StoresLocations.FindAsync(inputModel.Id);

            if (store == null) return false;

            store.Name = inputModel.Name;
            store.GoogleMapsUrl = inputModel.GoogleMapsUrl;
            store.Address = inputModel.Address;
            store.PhoneNumber = inputModel.Phone;
            store.CountryCode = inputModel.CountryCode;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var store = await _context.StoresLocations.FindAsync(id);

            if (store == null) return false;

            _context.StoresLocations.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
