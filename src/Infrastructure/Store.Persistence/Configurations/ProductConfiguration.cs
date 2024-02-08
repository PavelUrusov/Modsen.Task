using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products")
            .HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(1023);

        builder.Property(p => p.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.Quantity)
            .HasDefaultValue(0);

        builder.HasCheckConstraint("CK_Product_Quantity", "\"Quantity\" >= 0");
        builder.HasCheckConstraint("CK_Product_Price", "\"Price\" > 0.00");

        builder.HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity(pc => pc.ToTable("ProductCategories"));
    }
}