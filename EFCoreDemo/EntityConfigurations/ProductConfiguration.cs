using System.Collections.Generic;
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

            builder.HasData(new List<Product>()
            {
                new Product() {Id = 1, Name = "Laptop"},
                new Product() {Id = 2, Name = "Phone"},
                new Product() {Id = 3, Name = "Headphones"}
            });
        }
    }
}
