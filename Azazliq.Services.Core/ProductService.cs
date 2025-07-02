using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.ViewModels.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Configurations;
using Azaliq.Services.Core.Contracts;

namespace Azaliq.Services.Core
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync(string? userId)
        {
            IEnumerable<ProductIndexViewModel> allProducts = await _dbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .AsNoTracking()
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    IsSameDayDeliveryAvailable = p.IsSameDayDeliveryAvailable
                })
                .ToArrayAsync();

            return allProducts;
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsAsync(int? id, string? userId)
        {
            ProductDetailsViewModel? detailsVm = null;

            if (id.HasValue)
            {

                Product? product = await this._dbContext
                    .Products
                    .Include(p => p.Category)
                    .Include(p => p.Tags)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(r => r.Id == id.Value);

                if (product != null)
                {

                    detailsVm = new ProductDetailsViewModel()
                    {
                        Id = product.Id.ToString(),
                        Name = product.Name,
                        ImageUrl = product.ImageUrl,
                        Category = product.Category.Name,
                        Description = product.Description,
                        Price = product.Price.ToString("F2"),
                        IsSameDayDeliveryAvailable = product.IsSameDayDeliveryAvailable
                    };

                }

            }

            return detailsVm;
        }

        public async Task<bool> CreateProductAsync(string userId, CreateProductInputModel inputModel)
        {
            bool opResult = false;

            IdentityUser? user = await this._userManager
                .FindByIdAsync(userId);

            Category? category = await this._dbContext
                .Categories
                .FindAsync(inputModel.CategoryId);


            if ((user != null) && (category != null))
            {

                Product product = new Product()
                {
                    Name = inputModel.Name,
                    ImageUrl = inputModel.ImageUrl ?? string.Empty,
                    CategoryId = inputModel.CategoryId,
                    Description = inputModel.Description ?? string.Empty,
                    Price = inputModel.Price,
                    IsSameDayDeliveryAvailable = inputModel.IsSameDayDeliveryAvailable,
                    Tags = inputModel.SelectedTags?.Select(t => new ProductTag
                    {
                        Name = t
                    }).ToList() ?? new List<ProductTag>()
                };

                await this._dbContext.Products.AddAsync(product);
                await this._dbContext.SaveChangesAsync();

                opResult = true;
            }
            return opResult;
        }
    }
}
