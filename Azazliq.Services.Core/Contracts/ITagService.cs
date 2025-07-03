using Azaliq.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.ViewModels.Tag;

namespace Azaliq.Services.Core.Contracts
{
    public interface ITagService
    {

        Task<IEnumerable<TagIndexViewModel>> GetAllTagsAsync();

        Task AddTagAsync(CreateTagInputModel model);

        Task<TagEditInputModel?> GetTagByIdAsync(int? id);

        Task<bool> UpdateTagAsync(TagEditInputModel model);

        Task<DeleteTagInputModel?> GetTagForDeletionAsync(int? id);

        Task<bool> DeleteTagAsync(int id);

    }
}
