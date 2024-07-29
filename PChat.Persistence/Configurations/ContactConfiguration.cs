using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistence.Configurations;

public sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.User)
            .WithMany(u => u.ContactUsers)
            .HasForeignKey(c => c.UserCode)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Contact_User"); // Use Cascade or SetNull depending on your requirements

        builder.HasOne(c => c.UserContact)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.ContactCode)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Contact_User1"); // Use Cascade or SetNull depending on your requirements
    }
}