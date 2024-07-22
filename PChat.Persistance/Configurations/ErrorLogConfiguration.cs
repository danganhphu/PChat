using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PChat.Domain.Entities;

namespace PChat.Persistance.Configurations;

public sealed class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> builder)
    {
        builder.HasKey(c => c.ErrorId);

        builder.Property(c => c.ErrorId)
            .IsRequired()
            .HasMaxLength(36)
            .IsUnicode(false);
    }
}