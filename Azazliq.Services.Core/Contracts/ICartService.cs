using Azaliq.ViewModels.CartItems;

namespace Azaliq.Services.Core.Contracts
{
    public interface ICartService
    {
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task<List<CartItemViewModel>> GetCartItemsAsync(string userId);
        Task RemoveFromCartAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
        Task UpdateQuantityAsync(string userId, int productId, int quantity);

    }
}
