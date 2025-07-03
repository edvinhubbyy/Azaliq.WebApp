using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Tag;
using Azure;
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
                .Include(t => t.Products) // Include related products
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null) return null;

            return new DeleteTagInputModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                ProductNames = tag.Products.Select(p => p.Name).ToList() // List of product names using this tag
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


        public async Task<bool> AddOrUpdateProductTagsAsync(Product product, List<string>? selectedTagNames)
        {
            if (selectedTagNames == null)
            {
                product.Tags.Clear();
                return true;
            }

            // Normalize tag names (optional)
            var normalizedTags = selectedTagNames.Select(t => t.Trim()).Where(t => t != "").Distinct().ToList();

            // Load all tags in DB with these names
            var existingTags = await _dbContext.ProductsTags
                .Where(t => normalizedTags.Contains(t.Name))
                .ToListAsync();

            // Find names not existing yet
            var newTagNames = normalizedTags
                .Except(existingTags.Select(t => t.Name))
                .ToList();

            // Create new tag entities
            var newTags = newTagNames.Select(name => new ProductTag() { Name = name }).ToList();

            // Add new tags to DB
            if (newTags.Any())
            {
                await _dbContext.ProductsTags.AddRangeAsync(newTags);
                await _dbContext.SaveChangesAsync();
                existingTags.AddRange(newTags);
            }

            // Clear current product tags and assign new list
            product.Tags.Clear();
            foreach (var tag in existingTags)
            {
                product.Tags.Add(tag);
            }

            return true;
        }

    }
}
