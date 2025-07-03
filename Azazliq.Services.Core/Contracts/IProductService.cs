using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.ViewModels.Product;

namespace Azaliq.Services.Core.Contracts
{
    public interface IProductService
    {

        Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync(string? userId);

        Task<ProductDetailsViewModel> GetProductDetailsAsync(int? id, string? userId);

        Task<bool> CreateProductAsync(string userId, CreateProductInputModel inputModel);

        Task<EditProductInputModel?> EditProductAsync(string? userId, int? productId);

        Task<bool> PersistUpdateProductAsync(string userId, EditProductInputModel inputModel);

        Task<bool> SoftDeleteProductAsync(int productId);

        Task<DeleteProductModel?> GetProductForDeletionAsync(int? productId);


    }
}
