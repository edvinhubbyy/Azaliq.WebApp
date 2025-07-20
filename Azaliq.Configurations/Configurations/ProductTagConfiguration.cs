using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.ProductTag;

namespace Azaliq.Data.Configurations
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> entity)
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            // Many-to-many with Product, using EF Core conventions
            entity.HasMany(t => t.Products)
                .WithMany(p => p.Tags);

            entity.HasData(GetSeedTags());

        }
        
        private static ProductTag[] GetSeedTags()
        {
            return new[]
            {
                new ProductTag { Id = 1, Name = "Fresh" },
                new ProductTag { Id = 2, Name = "Popular" },
                new ProductTag { Id = 3, Name = "Seasonal" },
                new ProductTag { Id = 4, Name = "Gift" },
                new ProductTag { Id = 5, Name = "Fragrant" },
                new ProductTag { Id = 6, Name = "Wedding" },
                new ProductTag { Id = 7, Name = "Decor" },
                new ProductTag { Id = 8, Name = "Romantic" },
                new ProductTag { Id = 9, Name = "Exotic" },
                new ProductTag { Id = 10, Name = "Cheap" }
            };
        }
    }
}
