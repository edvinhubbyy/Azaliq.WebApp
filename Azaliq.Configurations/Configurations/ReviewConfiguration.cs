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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entity)
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Comment)
                .HasMaxLength(1000);

            entity.Property(r => r.Rating)
                .IsRequired();

            entity.Property(r => r.CreatedOn)
                .IsRequired();

            entity.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)  
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(r => r.IsDeleted == false);
        }

    }
}
