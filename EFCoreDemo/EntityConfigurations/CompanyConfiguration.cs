using System;
using System.Collections.Generic;
using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("CompanyId");
            builder.Property(p => p.Name).IsRequired().HasColumnName("Name").HasMaxLength(30);
            builder.Property(p => p.Revenue).HasColumnName("Revenue").HasColumnType("money");
            builder.Property(p => p.FoundationDate).IsRequired().HasColumnName("FoundationDate").HasColumnType("date");

            builder.HasMany(c => c.Products)
                .WithMany(s => s.Companies)
                .UsingEntity<Dictionary<string, object>>(
                    "Supply",
                    j => j
                        .HasOne<Product>()
                        .WithMany()
                        .HasForeignKey("ProductId"),
                    j => j
                        .HasOne<Company>()
                        .WithMany()
                        .HasForeignKey("CompanyId"));

            builder.HasData(new List<Company>()
            {
                new Company() {Id = 1, Name = "Google", FoundationDate = new DateTime(1998, 09, 04)},
                new Company() {Id = 2, Name = "Microsoft", FoundationDate = new DateTime(1975, 04, 04)}
            });
        }
    }
}
