using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Context.Configuration;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.HasKey(e => e.RolId).HasName("PK_rol");

        builder.ToTable("Rol");

        builder.HasIndex(e => e.Name, "UQ__Rol__72E12F1BC144BA24").IsUnique();

        builder.Property(e => e.RolId).HasColumnName("rolId");
        builder.Property(e => e.Name).HasMaxLength(50).IsUnicode(false).HasColumnName("name");
    }
}
