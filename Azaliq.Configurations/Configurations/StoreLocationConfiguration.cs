using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.StoreLocation;

namespace Azaliq.Data.Configurations
{
    public class StoreLocationConfiguration : IEntityTypeConfiguration<StoreLocation>
    {
        public void Configure(EntityTypeBuilder<StoreLocation> entity)
        {
            entity.HasKey(sl => sl.Id);

            entity.Property(sl => sl.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            entity.Property(sl => sl.GoogleMapsUrl)
                .IsRequired()
                .HasMaxLength(GoogleMapsUrlLength);

            entity.Property(sl => sl.Address)
                .IsRequired()
                .HasMaxLength(AddressMaxLength);

            entity.Property(sl => sl.Phone)
                .HasMaxLength(PhoneMaxLength);
        }
    }
    
}
