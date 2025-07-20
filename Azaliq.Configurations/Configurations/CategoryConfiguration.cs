using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.Category;

namespace Azaliq.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {

            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .HasMaxLength(NameMaxLength)
                .IsRequired();

            entity
                .HasQueryFilter(c => c.IsDeleted == false);

            entity.HasData(new Category
            {
                Id = -1,
                Name = "Deleted Category",
                IsDeleted = true
            });

            entity.HasData(GetSeedCategories());

        }
        
        private static Category[] GetSeedCategories()
        {
            return new[]
            {
                new Category { Id = 1, Name = "Roses" },
                new Category { Id = 2, Name = "Tulips" },
                new Category { Id = 3, Name = "Lilies" },
                new Category { Id = 4, Name = "Orchids" },
                new Category { Id = 5, Name = "Sunflowers" },
                new Category { Id = 6, Name = "Carnations" },
                new Category { Id = 7, Name = "Daisies" },
                new Category { Id = 8, Name = "Peonies" },
                new Category { Id = 9, Name = "Chrysanthemums" },
                new Category { Id = 10, Name = "Gardenias" }
            };
        }
    }
}
