using PChat.Domain.Abstractions;

namespace PChat.Domain.Entities;

public sealed class Contact : BaseEntity
{
    public long Id { get; set; }
    public string? UserCode { get; set; }
    public string? ContactCode { get; set; }

    public User? User { get; set; }

    public User? UserContact { get; set; }
}