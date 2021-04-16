using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("UserId");
            builder.Property(p => p.FirstName).IsRequired().HasColumnName("FirstName").HasMaxLength(20);
            builder.Property(p => p.LastName).IsRequired().HasColumnName("LastName").HasMaxLength(20);
            builder.Property(p => p.HiredDate).IsRequired().HasColumnName("HiredDate").HasColumnType("smalldatetime");

            builder.HasOne(d => d.Company)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
