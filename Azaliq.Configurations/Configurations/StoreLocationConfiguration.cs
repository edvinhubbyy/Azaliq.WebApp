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
    public class StoreLocationConfiguration : IEntityTypeConfiguration<StoreLocation>
    {
        public void Configure(EntityTypeBuilder<StoreLocation> entity)
        {
            entity.HasKey(sl => sl.Id);

            entity.Property(sl => sl.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(sl => sl.GoogleMapsUrl)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(sl => sl.Address)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(sl => sl.Phone)
                .HasMaxLength(50);
        }
    }
    
}
