using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{

    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens")
            .HasKey(rt => rt.Id);

        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(rt => rt.CreatedOn)
            .IsRequired();

        builder.Property(rt => rt.ExpiryOn)
            .IsRequired();

        builder.Property(rt => rt.CreatedByIp)
            .IsRequired()
            .HasMaxLength(63);

        builder.Property(rt => rt.RevokedByIp)
            .HasMaxLength(63)
            .IsRequired(false);

        builder.Property(rt => rt.RevokedOn)
            .IsRequired(false);

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}