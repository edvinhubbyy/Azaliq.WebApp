using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.Product;

namespace Azaliq.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);
            
            entity
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Description)
                .HasMaxLength(DescriptionMaxLength);

            entity.Property(p => p.ImageUrl)
                .HasMaxLength(ImageUrlMaxLength);

            entity.Property(p => p.Price)
                .IsRequired();

            entity.Property(p => p.IsAvailable)
                .IsRequired();

            entity
                .HasOne(r => r.Category)
                .WithMany(r => r.Products)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.IsSameDayDeliveryAvailable)
                .IsRequired();

            entity.HasMany(p => p.Tags)
                .WithMany(t => t.Products);

            entity
                .HasQueryFilter(p => p.IsDeleted == false);

        }
    }
}
