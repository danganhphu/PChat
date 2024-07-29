using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistence.Configurations;

public sealed class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.HasKey(gu => gu.Id);

        builder.Property(gu => gu.GroupCode)
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
            .OnDelete(DeleteBehavior.SetNull);
    }
}