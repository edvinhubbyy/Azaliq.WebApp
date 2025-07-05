using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azaliq.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> entity)
        {
            // Primary key
            entity.HasKey(ci => ci.Id);

            // Quantity is required, default 1
            entity.Property(ci => ci.Quantity)
                .IsRequired();

            // Relation to Product: many CartItems can reference one Product
            entity.HasOne(ci => ci.Product)
                .WithMany()       // no navigation property in Product
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation to ApplicationUser: many CartItems can reference one User
            entity.HasOne(ci => ci.User)
                .WithMany()       // no navigation property in ApplicationUser
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique index to prevent duplicate CartItems for same user and product
            entity.HasIndex(ci => new { ci.UserId, ci.ProductId })
                .IsUnique();

        }
    }
}
