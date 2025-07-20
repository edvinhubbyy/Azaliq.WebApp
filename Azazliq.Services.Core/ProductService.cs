using Azaliq.Data;
using Azaliq.Data.Configurations;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Product;
using Azaliq.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Services.Core
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _dbContext;
        //private readonly UserManager<ApplicationUserProduct> _userManager;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                    Quantity = p.Quantity,
                    IsSameDayDeliveryAvailable = p.IsSameDayDeliveryAvailable
                })
                .ToArrayAsync();

            return allProducts;
        }

        public async Task<IEnumerable<ProductDetailsViewModel>> GetProductsByTagAsync(string tagName)
        {
            return await _dbContext.Products
                .Where(p => p.Tags.Any(t => t.Name == tagName))
                .Select(p => new ProductDetailsViewModel
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price.ToString("F2"),
                    Quantity = p.Quantity,
                    Category = p.Category.Name,
                    IsAvailable = p.IsAvailable,
                    IsSameDayDeliveryAvailable = p.IsSameDayDeliveryAvailable,
                    Tags = p.Tags.Select(t => t.Name).ToList(),
                    Reviews = p.Reviews.Select(r => new ReviewViewModel
                    {
                        // map review properties here
                    }).ToList()
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<ProductListItemViewModel>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _dbContext.Products
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .Select(p => new ProductListItemViewModel
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price.ToString("F2"),  // Format price as string with 2 decimals
                    IsAvailable = p.Quantity > 0
                })
                .ToListAsync();
        }


        public async Task<ProductDetailsViewModel> GetProductDetailsAsync(int? id, string? userId)
        {
            ProductDetailsViewModel? detailsVm = null;

            if (id.HasValue)
            {
                var product = await this._dbContext
                    .Products
                    .Include(p => p.Category)
                    .Include(p => p.Tags)
                    .Include(p => p.Reviews)            // include reviews navigation
                    .ThenInclude(r => r.User)       // include user navigation inside reviews
                    .AsNoTracking()
                    .SingleOrDefaultAsync(r => r.Id == id.Value && !r.IsDeleted);

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
                        Quantity = product.Quantity,
                        IsSameDayDeliveryAvailable = product.IsSameDayDeliveryAvailable,

                        Tags = product.Tags.Select(t => t.Name).ToList(),

                        Reviews = product.Reviews
                            .Where(r => !r.IsDeleted)
                            .Select(r => new ReviewViewModel
                            {
                                Id = r.Id,
                                UserName = r.User.UserName,  // from navigation User
                                Comment = r.Comment,
                                Rating = r.Rating,
                                CreatedOn = r.CreatedOn
                            })
                            .ToList()
                    };
                }
            }

            return detailsVm!;
        }


        public async Task<bool> CreateProductAsync(string userId, CreateProductInputModel inputModel)
        {
            bool opResult = false;

            Category? category = await this._dbContext.Categories.FindAsync(inputModel.CategoryId);

            if (category != null)
            {
                // Get existing tags from DB that match selected tag names
                var existingTags = await _dbContext.ProductsTags
                    .Where(t => inputModel.SelectedTags.Contains(t.Name))
                    .ToListAsync();

                // Find tag names that are new (not in existingTags)
                var newTagNames = inputModel.SelectedTags
                    .Except(existingTags.Select(t => t.Name))
                    .Distinct();

                // Create new tag entities for those new tag names
                var newTags = newTagNames.Select(name => new ProductTag { Name = name }).ToList();

                // Combine existing tags and new tags into one list
                var allTags = existingTags.Concat(newTags).ToList();

                // Create new product and assign tags collection
                Product product = new Product()
                {
                    Name = inputModel.Name,
                    ImageUrl = inputModel.ImageUrl ?? string.Empty,
                    CategoryId = inputModel.CategoryId,
                    Description = inputModel.Description ?? string.Empty,
                    Price = inputModel.Price,
                    Quantity = inputModel.Quantity,
                    IsSameDayDeliveryAvailable = inputModel.IsSameDayDeliveryAvailable,
                    Tags = allTags
                };

                // Add product (and new tags if any) to database
                await this._dbContext.Products.AddAsync(product);

                // Save changes
                await this._dbContext.SaveChangesAsync();

                opResult = true;
            }

            return opResult;
        }


        public async Task<EditProductInputModel?> EditProductAsync(string? userId, int? productId)
        {
            if (productId == null)
                return null;

            var product = await _dbContext.Products
                .Include(p => p.Tags)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return null;

            var model = new EditProductInputModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                IsSameDayDeliveryAvailable = product.IsSameDayDeliveryAvailable,
                CategoryId = product.CategoryId,
                SelectedTags = product.Tags.Select(t => t.Name).ToList(),  // Populate selected tags here
                AllTags = await _dbContext.ProductsTags.Select(t => t.Name).ToListAsync(), // All possible tags
            };

            return model;
        }

        public async Task<bool> PersistUpdateProductAsync(string userId, EditProductInputModel inputModel)
        {
            var product = await _dbContext.Products
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == inputModel.Id);

            if (product == null)
                return false;

            product.Name = inputModel.Name;
            product.Description = inputModel.Description;
            product.ImageUrl = inputModel.ImageUrl;
            product.Price = inputModel.Price;
            product.Quantity = inputModel.Quantity;
            product.IsSameDayDeliveryAvailable = inputModel.IsSameDayDeliveryAvailable;
            product.CategoryId = inputModel.CategoryId;

            // --- Handle tag updates ---

            // Normalize input selected tags list (avoid null)
            var selectedTagNames = inputModel.SelectedTags?.Distinct().ToList() ?? new List<string>();

            // Current tag names on product
            var currentTagNames = product.Tags.Select(t => t.Name).ToList();

            // Tags to remove (no longer selected)
            var tagsToRemove = product.Tags.Where(t => !selectedTagNames.Contains(t.Name)).ToList();

            // Remove unselected tags
            foreach (var tag in tagsToRemove)
            {
                product.Tags.Remove(tag);
            }

            // Tags to add (newly selected)
            var tagsToAddNames = selectedTagNames.Except(currentTagNames).ToList();

            foreach (var tagName in tagsToAddNames)
            {
                // Check if the tag already exists in database (optional)
                var existingTag = await _dbContext.ProductsTags.FirstOrDefaultAsync(t => t.Name == tagName);

                if (existingTag != null)
                {
                    product.Tags.Add(existingTag);
                }
                else
                {
                    // Create new tag if it doesn't exist
                    var newTag = new ProductTag { Name = tagName };
                    product.Tags.Add(newTag);
                }
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }



        public async Task<DeleteProductModel?> GetProductForDeletionAsync(int? productId)
        {
            if (productId == null)
                return null;

            var product = await _dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return null;

            return new DeleteProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                IsSameDayDeliveryAvailable = product.IsSameDayDeliveryAvailable,
                CategoryId = product.CategoryId,
                // fill other props if needed
            };
        }

        // Soft delete method:
        public async Task<bool> SoftDeleteProductAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) return false;

            product.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
