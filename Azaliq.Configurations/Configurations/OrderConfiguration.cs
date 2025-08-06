using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.Cart;

namespace Azaliq.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity
                .HasKey(o => o.Id);

            entity
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.PickupStore)
                .WithMany(s => s.PickupOrders)
                .HasForeignKey(o => o.PickupStoreId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            entity
                .Property(o => o.OrderDate)
                .IsRequired();

            entity.Property(o => o.PickupTime)
                .IsRequired(false);

            entity.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired();

            entity.Property(o => o.TotalAmount)
                .IsRequired();

            entity.Property(o => o.IsDelivery)
                .IsRequired();

            entity.Property(o => o.DeliveryAddress)
                .HasMaxLength(200)
                .IsRequired(false);
            entity.HasMany(o => o.Products)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Restrict);


            entity.Property(o => o.FullName)
                .IsRequired()
                .HasMaxLength(FullNameMaxLength); // Use your validation constants here

            entity.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(EmailMaxLength);

            entity.Property(o => o.Phone)
                .IsRequired()
                .HasMaxLength(PhoneMaxLength);

            entity.Property(o => o.City)
                .IsRequired(false)
                .HasMaxLength(CityMaxLength);

            entity.Property(o => o.ZipCode)
                .IsRequired(false)
                .HasMaxLength(ZipCodeMaxLength);

            entity.Property(o => o.CountryCode)
                .IsRequired();

            entity.Property(o => o.DeliveryOption)
                .IsRequired();

            entity.Property(o => o.OrderDate)
                .IsRequired();


            entity
                .HasQueryFilter(o => o.IsDeleted == false);

        }
    }
}
