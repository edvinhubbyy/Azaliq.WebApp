using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azaliq.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.HasKey(o => o.Id);

            // Relationship with User
            entity.HasOne(o => o.User)
                .WithMany() // or .WithMany(u => u.Orders) if you add navigation property in ApplicationUser
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDate is required
            entity.Property(o => o.OrderDate)
                .IsRequired();

            // PickupTime optional
            entity.Property(o => o.PickupTime)
                .IsRequired(false);

            // OrderStatus enum stored as string (optional)
            entity.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired();

            // TotalAmount required with precision (e.g., 18,2)
            entity.Property(o => o.TotalAmount)
                .IsRequired();

            // IsDelivery required
            entity.Property(o => o.IsDelivery)
                .IsRequired();

            // DeliveryAddress optional with max length
            entity.Property(o => o.DeliveryAddress)
                .HasMaxLength(200)
                .IsRequired(false);

            // Relationship with OrderProduct - one-to-many
            entity.HasMany(o => o.Products)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasQueryFilter(o => o.IsDeleted == false);
        }
    }
}
