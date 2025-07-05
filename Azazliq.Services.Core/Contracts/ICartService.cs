using Azaliq.Data.Models.Models;
using Azaliq.ViewModels.CartItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
