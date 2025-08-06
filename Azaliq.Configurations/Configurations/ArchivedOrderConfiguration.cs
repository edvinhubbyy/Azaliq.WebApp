using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azaliq.Data.Configurations
{
    public class ArchivedOrderConfiguration : IEntityTypeConfiguration<ArchivedOrder>
    {
        public void Configure(EntityTypeBuilder<ArchivedOrder> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasMany(o => o.Products)
                .WithOne(p => p.ArchivedOrder)
                .HasForeignKey(p => p.ArchivedOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
