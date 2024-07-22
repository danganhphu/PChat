using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistance.Configurations;

public sealed class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.HasKey(gu => gu.Id);

        builder.Property(gu => gu.GroupCode)
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.Property(gu => gu.UserCode)
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.HasIndex(gu => new { gu.GroupCode, gu.UserCode })
            .IsUnique();

        builder.HasOne(gu => gu.Group)
            .WithMany(g => g.GroupUsers)
            .HasForeignKey(gu => gu.GroupCode)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(gu => gu.User)
            .WithMany(u => u.GroupUsers)
            .HasForeignKey(gu => gu.UserCode)
            .HasPrincipalKey(u => u.Code)
            .OnDelete(DeleteBehavior.SetNull);
    }
}