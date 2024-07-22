using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistance.Configurations;

public sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Code);
        builder.Property(u => u.Code)
            .IsRequired()
            .HasMaxLength(36)
            .IsUnicode(false);
        
        builder.Property(g => g.Type)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);

        builder.Property(g => g.Avatar)
            .HasMaxLength(200);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(g => g.CreatedBy)
            .IsRequired()
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.Property(g => g.LastActive)
            .IsRequired();

        builder.HasMany(g => g.GroupUsers)
            .WithOne(gu => gu.Group)
            .HasForeignKey(gu => gu.GroupCode)
            .OnDelete(DeleteBehavior.SetNull); // Use Cascade or SetNull depending on your requirements

        builder.HasMany(g => g.Messages)
            .WithOne(m => m.Group)
            .HasForeignKey(m => m.GroupCode)
            .OnDelete(DeleteBehavior.SetNull); // Use Cascade or SetNull depending on your requirements
    }
}