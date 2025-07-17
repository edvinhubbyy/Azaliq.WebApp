using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azaliq.Data.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> entity)
        {
            entity
                .HasKey(m => m.Id);

            entity
                .Property(m => m.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(m => m.User)
                .WithOne()
                .HasForeignKey<Manager>(m => m.UserId);

            entity
                .HasIndex(m => new { m.UserId })
                .IsUnique();

            entity
                .HasMany(m => m.ManagedStores)
                .WithOne(c => c.Manager)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasQueryFilter(m => m.IsDeleted == false);
        }
    }
}
