using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistance.Configurations;

public sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserCode)
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.Property(c => c.ContactCode)
            .HasMaxLength(36)
            .IsUnicode(false);

        builder.HasOne(c => c.User)
            .WithMany(u => u.ContactUsers)
            .HasForeignKey(c => c.UserCode)
            .HasPrincipalKey(u => u.Code)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Contact_User"); // Use Cascade or SetNull depending on your requirements

        builder.HasOne(c => c.UserContact)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.ContactCode)
            .HasPrincipalKey(u => u.Code)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Contact_User1"); // Use Cascade or SetNull depending on your requirements
    }
}