using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.EntityConfigurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfile").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("UserProfileId");
            builder.Property(p => p.About).HasColumnName("About").HasMaxLength(2000);
            builder.Property(p => p.ImageUrl).IsRequired().HasColumnName("ImageUrl").HasMaxLength(255);
        }
    }
}
