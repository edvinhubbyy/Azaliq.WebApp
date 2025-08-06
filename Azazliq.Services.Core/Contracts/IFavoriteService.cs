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
