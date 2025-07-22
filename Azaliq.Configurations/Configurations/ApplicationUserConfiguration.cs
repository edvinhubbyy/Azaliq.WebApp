using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azaliq.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            
            // Configure FullName as required with max length
            entity.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(150);

            // Address is optional, set max length
            entity.Property(u => u.Address)
                .HasMaxLength(300);

            // One-to-many relation with Orders
            entity
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .Property(u => u.IsBanned)
                .HasDefaultValue(false);

        }
    }
    
}
