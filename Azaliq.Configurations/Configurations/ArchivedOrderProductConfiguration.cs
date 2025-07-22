using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azaliq.Data.Configurations
{
    public class ArchivedOrderProductConfiguration : IEntityTypeConfiguration<ArchivedOrderProduct>
    {
        public void Configure(EntityTypeBuilder<ArchivedOrderProduct> builder)
        {
            // Primary key
            builder.HasKey(aop => aop.Id);

            // Relationship: many ArchivedOrderProducts to one ArchivedOrder
            builder
                .HasOne(aop => aop.ArchivedOrder)
                .WithMany(ao => ao.Products)
                .HasForeignKey(aop => aop.ArchivedOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductName is required, max length 200 (adjust as needed)
            builder.Property(aop => aop.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            // Price with decimal precision and scale
            builder.Property(aop => aop.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Quantity required
            builder.Property(aop => aop.Quantity)
                .IsRequired();
        }
    }
}
