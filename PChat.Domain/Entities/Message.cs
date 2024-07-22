using System.ComponentModel.DataAnnotations.Schema;
using PChat.Domain.Abstractions;

namespace PChat.Domain.Entities;

public sealed class Message: BaseEntity
{
    public long Id { get; set; }

    public string Type { get; set; } = string.Empty; // text, media, attachment

    public string? GroupCode { get; set; }

    public string Content { get; set; } = string.Empty;

    public string? Path { get; set; }

    public string? CreatedBy { get; set; }

    public Group? Group { get; set; }

    public User? UserCreatedBy { get; set; }
}