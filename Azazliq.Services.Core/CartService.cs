using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.CartItems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Services.Core
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                await _context.CartItems.AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItemViewModel>> GetCartItemsAsync(string userId)
        {
            // Example implementation
            return await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Select(ci => new CartItemViewModel
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    ProductImageUrl = ci.Product.ImageUrl,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity
                })
                .ToListAsync();
        }
        // Implement other methods as needed


        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = _context.CartItems.Where(ci => ci.UserId == userId);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuantityAsync(string userId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);

            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
                await _context.SaveChangesAsync();
            }
        }
        
    }

}
