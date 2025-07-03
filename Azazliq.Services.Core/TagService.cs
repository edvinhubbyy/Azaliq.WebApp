using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Tag;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Services.Core
{
    public class TagService : ITagService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TagService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<TagIndexViewModel>> GetAllTagsAsync()
        {
            var tags = await _dbContext.ProductsTags
                .AsNoTracking()
                .Select(t => new TagIndexViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return tags;
        }

        public async Task AddTagAsync(CreateTagInputModel model)
        {
            var tag = new ProductTag()
            {
                Name = model.Name
            };

            _dbContext.ProductsTags.Add(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TagEditInputModel?> GetTagByIdAsync(int? id)
        {
            TagEditInputModel? editModel = null;

            if (id != null)
            {

                ProductTag? editTagModel = await this._dbContext
                    .ProductsTags
                    .Include(r => r.Products)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(r => r.Id == id);

                if (editTagModel != null)
                {
                    editModel = new TagEditInputModel
                    {
                        Id = editTagModel.Id,
                        Name = editTagModel.Name,
                    };
                }
            }

            return editModel;
        }

        public async Task<bool> UpdateTagAsync(TagEditInputModel model)
        {
            bool opResult = false;

            ProductTag? updateTag = await this._dbContext
                .ProductsTags
                .FindAsync(model.Id);


            if (updateTag != null)
            {

                updateTag.Id = model.Id;
                updateTag.Name = model.Name;

                await this._dbContext.SaveChangesAsync();

                opResult = true;

            }

            return opResult;
        }

        public async Task<DeleteTagInputModel?> GetTagForDeletionAsync(int? id)
        {
            if (id == null) return null;

            var tag = await _dbContext.ProductsTags
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null) return null;

            return new DeleteTagInputModel()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var tag = await _dbContext.ProductsTags.FindAsync(id);

            if (tag == null) return false;

            _dbContext.ProductsTags.Remove(tag);

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                // log exception
                return false;
            }
        }
    }
}
