using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.ViewModels.Product;

namespace Azaliq.Services.Core.Contracts
{
    public interface IFavoriteService
    {
        Task AddToFavoritesAsync(string userId, int productId);
        
        Task RemoveFromFavoritesAsync(string userId, int productId);
        
        Task<bool> IsFavoriteAsync(string userId, int productId);
        
        Task<IEnumerable<ProductIndexViewModel>> GetFavoritesAsync(string userId);
    }

}
