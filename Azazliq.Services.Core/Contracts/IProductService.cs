using Azaliq.ViewModels.Product;

namespace Azaliq.Services.Core.Contracts
{
    public interface IProductService
    {

        Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync(string? userId, string? searchTerm = null);

        Task<IEnumerable<ProductListItemViewModel>> GetProductsByCategoryAsync(int categoryId);

        Task<IEnumerable<ProductDetailsViewModel>> GetProductsByTagAsync(string tagName);

        Task<ProductDetailsViewModel> GetProductDetailsAsync(int? id, string? userId);

        Task<bool> CreateProductAsync(string userId, CreateProductInputModel inputModel);

        Task<EditProductInputModel?> EditProductAsync(string? userId, int? productId);

        Task<bool> PersistUpdateProductAsync(string userId, EditProductInputModel inputModel);

        Task<bool> SoftDeleteProductAsync(int productId);

        Task<DeleteProductModel?> GetProductForDeletionAsync(int? productId);


    }
}
