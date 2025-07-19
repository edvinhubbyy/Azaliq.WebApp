using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.StoreLocation;

namespace Azaliq.Data.Configurations
{
    public class StoreLocationConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity
                .HasKey(sl => sl.Id);

            entity
                .Property(sl => sl.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            entity
                .Property(sl => sl.GoogleMapsUrl)
                .IsRequired()
                .HasMaxLength(GoogleMapsUrlLength);

            entity
                .Property(sl => sl.Address)
                .IsRequired()
                .HasMaxLength(AddressMaxLength);

            entity
                .Property(sl => sl.PhoneNumber)
                .HasMaxLength(PhoneMaxLength);
        }
    }
    
}
