using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo.EntityConfigurations
{
    public class SupplyHistoryConfiguration : IEntityTypeConfiguration<SupplyHistory>
    {
        public void Configure(EntityTypeBuilder<SupplyHistory> builder)
        {
            builder.ToTable("SupplyHistory").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("SupplyHistoryId");
            builder.Property(p => p.ShipmentDate).IsRequired().HasColumnName("ShipmentDate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.Price).IsRequired().HasColumnName("Price").HasColumnType("money");

            builder.HasOne(d => d.Company)
                .WithMany(p => p.SupplyHistory)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.SupplyHistory)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
