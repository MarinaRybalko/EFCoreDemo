using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ProductId");
            builder.Property(p => p.Name).IsRequired().HasColumnName("Name").HasMaxLength(255);
        }
    }
}
