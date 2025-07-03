using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.Category;

namespace Azaliq.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {

            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .HasMaxLength(NameMaxLength)
                .IsRequired();

            entity
                .HasQueryFilter(c => c.IsDeleted == false);

            entity.HasData(new Category
            {
                Id = -1,
                Name = "Deleted Category",
                IsDeleted = true
            });

        }
    }
}
