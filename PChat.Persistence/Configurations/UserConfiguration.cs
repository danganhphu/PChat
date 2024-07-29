using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistence.Configurations;

public sealed class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(u => u.Dob)
            .HasMaxLength(10)
            .IsUnicode();

        builder.Property(u => u.Phone)
            .HasMaxLength(15)
            .IsUnicode();

        builder.Property(u => u.Address)
            .HasMaxLength(200)
            .IsUnicode();

        builder.Property(u => u.Avatar)
            .HasMaxLength(200)
            .IsUnicode();

        builder.Property(u => u.Gender)
            .HasMaxLength(10)
            .IsUnicode();

        builder.Property(u => u.LastLogin);

        builder.Property(u => u.CurrentSession)
            .HasMaxLength(250)
            .IsUnicode();

        builder.HasMany(u => u.Calls)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserCode)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.GroupUsers)
            .WithOne(gu => gu.User)
            .HasForeignKey(gu => gu.UserCode)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.Messages)
            .WithOne(m => m.UserCreatedBy)
            .HasForeignKey(m => m.CreatedBy)
            .OnDelete(DeleteBehavior.SetNull);
    }
}