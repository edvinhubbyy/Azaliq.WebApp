using Azaliq.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using Azaliq.Data;

namespace Azaliq.Services.Core
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToFavoritesAsync(string userId, int productId)
        {
            if (!await _context.Favorites.AnyAsync(f => f.UserId == userId && f.ProductId == productId))
            {
                _context.Favorites.Add(new Favorite { UserId = userId, ProductId = productId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromFavoritesAsync(string userId, int productId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsFavoriteAsync(string userId, int productId)
        {
            return await _context.Favorites.AnyAsync(f => f.UserId == userId && f.ProductId == productId);
        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => new ProductIndexViewModel
                {
                    Id = f.Product.Id,
                    Name = f.Product.Name,
                    Price = f.Product.Price,
                    ImageUrl = f.Product.ImageUrl
                })
                .ToListAsync();
        }
    }
}
