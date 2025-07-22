using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Azaliq.GCommon.ValidationConstants.Archives;

namespace Azaliq.Data.Configurations
{
    public class ArchivedUserConfiguration : IEntityTypeConfiguration<ArchivedUser>
    {
        public void Configure(EntityTypeBuilder<ArchivedUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever(); // Because you'll assign a new GUID manually

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(EmailMaxLength);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(UserNameMaxLength);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.ArchivedUser)
                .HasForeignKey(o => o.ArchivedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
