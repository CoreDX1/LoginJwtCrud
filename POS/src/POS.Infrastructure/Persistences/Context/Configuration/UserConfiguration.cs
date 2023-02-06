using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId).HasName("PK_user");

        builder.ToTable("User");

        builder.HasIndex(e => e.Email, "UQ__User__AB6E6164495D00C4").IsUnique();

        builder.Property(e => e.UserId).HasColumnName("userId");
        builder
            .Property(e => e.DateRegister)
            .HasColumnType("datetime")
            .HasColumnName("dateRegister");
        builder.Property(e => e.Email).HasMaxLength(100).IsUnicode(false).HasColumnName("email");
        builder
            .Property(e => e.LastName)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("lastName");
        builder.Property(e => e.Name).HasMaxLength(50).IsUnicode(false).HasColumnName("name");
        builder
            .Property(e => e.Password)
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("password");
        builder.Property(e => e.RolId).HasColumnName("rolId");
        builder.Property(e => e.Status).HasDefaultValueSql("((1))").HasColumnName("status");

        builder
            .HasOne(d => d.Rol)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.RolId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__User__rolId__3E52440B");
    }
}
