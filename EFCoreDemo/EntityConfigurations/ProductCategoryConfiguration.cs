using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.EntityConfigurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ProductCategoryId");
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.CreatedDate).HasColumnType("date").HasDefaultValueSql("GETDATE()");

            builder.HasMany(d => d.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
