using PChat.Domain.Abstractions;

namespace PChat.Domain.Entities;

public sealed class Call : BaseEntity
{
    public int Id { get; set; }

    public string? GroupCallCode { get; set; } // Allow null

    public string? UserCode { get; set; } // Allow null

    public string Url { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public GroupCall? GroupCall { get; set; } // Allow null

    public User? User { get; set; } // Allow null
}