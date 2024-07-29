using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistence.Configurations;

public sealed class GroupCallConfiguration : IEntityTypeConfiguration<GroupCall>
{
    public void Configure(EntityTypeBuilder<GroupCall> builder)
    {
        builder.HasKey(gc => gc.Code);
        builder.Property(gc => gc.Code)
            .HasMaxLength(36)
            .IsUnicode(false);
        
        builder.Property(gc => gc.Type)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);

        builder.Property(gc => gc.Avatar)
            .HasMaxLength(200);

        builder.Property(gc => gc.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(gc => gc.LastActive)
            .IsRequired();

        builder.HasMany(gc => gc.Calls)
            .WithOne(c => c.GroupCall)
            .HasForeignKey(c => c.GroupCallCode)
            .OnDelete(DeleteBehavior.SetNull); // Use SetNull if you want to retain calls with null GroupCallCode
    }
}