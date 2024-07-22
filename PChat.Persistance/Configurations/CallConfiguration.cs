using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistance.Configurations;

public sealed class CallConfiguration : IEntityTypeConfiguration<Call>
{
    public void Configure(EntityTypeBuilder<Call> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.GroupCallCode)
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.Property(c => c.UserCode)
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.Property(c => c.Url)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode();

        builder.Property(c => c.Status)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        builder.HasOne(c => c.GroupCall)
            .WithMany(gc => gc.Calls)
            .HasForeignKey(c => c.GroupCallCode)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.User)
            .WithMany(u => u.Calls)
            .HasForeignKey(c => c.UserCode)
            .HasPrincipalKey(u => u.Code)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Call_User");
    }
}