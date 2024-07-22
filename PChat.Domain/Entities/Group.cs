using PChat.Domain.Abstractions;

namespace PChat.Domain.Entities;

public sealed class Group: BaseEntity
{
    public required string Code { get; set; } = Guid.NewGuid().ToString();

    public string Type { get; set; } = string.Empty; // single: chat 1-1, multi: chat 1-n

    public string? Avatar { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime LastActive { get; set; }
    
    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
