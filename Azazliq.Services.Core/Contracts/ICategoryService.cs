using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.ViewModels.Category;
using Azaliq.ViewModels.Product;

namespace Azaliq.Services.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CreateProductDropDownCategory>> GetCategoryDropDownDataAsync();

        Task<string> GetCategoryNameAsync(int categoryId);

        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync(string? userId);

        Task AddCategoryAsync(AddCategoryViewModel model);

        Task<DeleteCategoryModel?> GetCategoryForDeletionAsync(int? categoryId);

        Task<(bool CanDelete, List<string> BlockingProducts)> CanDeleteCategoryAsync(int categoryId);

        Task<bool> DeleteCategoryAsync(int? categoryId);

        Task<EditCategoryInputModel?> EditCategoryAsync(string? userId, int? categoryId);

        Task<bool> PersistUpdateCategoryAsync(string userId, EditCategoryInputModel inputModel);



    }
}
