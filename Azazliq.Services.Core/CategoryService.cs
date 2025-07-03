using Azaliq.Data;
using Azaliq.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Configurations;
using Azaliq.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Azaliq.ViewModels.Category;

namespace Azaliq.Services.Core
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<CreateProductDropDownCategory>> GetCategoryDropDownDataAsync()
        {
            IEnumerable<CreateProductDropDownCategory> categories = await _dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new CreateProductDropDownCategory()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return categories;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync(string? userId)
        {
            var categories =  await _dbContext
                .Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return categories;
        }

        public async Task AddCategoryAsync(AddCategoryViewModel model)
        {
            var category = new Category
            {
                Name = model.Name
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<DeleteCategoryModel?> GetCategoryForDeletionAsync(int? categoryId)
        {
            if (categoryId == null)
                return null;

            var category = await _dbContext.Categories
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == categoryId);

            if (category == null)
                return null;

            return new DeleteCategoryModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> DeleteCategoryAsync(int? categoryId)
        {
            const int deletedCategoryId = -1;

            var category = await _dbContext.Categories
                .Include(c => c.Products)
                .SingleOrDefaultAsync(c => c.Id == categoryId && !c.IsDeleted && c.Id != deletedCategoryId);

            if (category == null)
                return false;

            foreach (var product in category.Products.Where(p => !p.IsDeleted))
            {
                product.CategoryId = deletedCategoryId;
            }

            category.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        // TEST: Check if a category can be deleted based on associated products

        public async Task<(bool CanDelete, List<string> BlockingProducts)> CanDeleteCategoryAsync(int categoryId)
        {
            var products = await _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => p.Name)
                .ToListAsync();

            bool canDelete = !products.Any();

            return (canDelete, products);
        }



        public async Task<EditCategoryInputModel?> EditCategoryAsync(string? userId, int? categoryId)
        {
            if (categoryId == null)
                return null;

            var category = await _dbContext.Categories
                .Include(c => c.Products)   // If you really need products, otherwise remove Include
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                return null;  // Handle category not found

            var model = new EditCategoryInputModel
            {
                Id = category.Id,
                Name = category.Name,
                // Add other properties if needed
            };

            return model;
        }


        public async Task<bool> PersistUpdateCategoryAsync(string userId, EditCategoryInputModel inputModel)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == inputModel.Id);

            if (category == null)
                return false; // Could not find category to update

            category.Name = inputModel.Name;

            await _dbContext.SaveChangesAsync();

            return true;
        }




    }
}
