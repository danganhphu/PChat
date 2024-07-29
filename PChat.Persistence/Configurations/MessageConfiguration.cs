using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistence.Configurations;

public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Type)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);

        builder.Property(m => m.GroupCode)
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(1000)
            .IsUnicode();

        builder.Property(m => m.Path)
            .HasMaxLength(200)
            .IsUnicode();

        builder.HasOne(m => m.Group)
            .WithMany(g => g.Messages)
            .HasForeignKey(m => m.GroupCode)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.UserCreatedBy)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.CreatedBy)
            .OnDelete(DeleteBehavior.SetNull);
    }
}