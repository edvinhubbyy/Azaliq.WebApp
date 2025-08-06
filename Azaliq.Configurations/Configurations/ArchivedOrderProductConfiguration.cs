using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azaliq.Data.Configurations
{
    public class ArchivedOrderProductConfiguration : IEntityTypeConfiguration<ArchivedOrderProduct>
    {
        public void Configure(EntityTypeBuilder<ArchivedOrderProduct> builder)
        {
            builder.HasKey(aop => aop.Id);
            builder
                .HasOne(aop => aop.ArchivedOrder)
                .WithMany(ao => ao.Products)
                .HasForeignKey(aop => aop.ArchivedOrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(aop => aop.ProductName)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(aop => aop.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(aop => aop.Quantity)
                .IsRequired();
        }
    }
}
