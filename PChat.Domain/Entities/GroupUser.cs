namespace PChat.Domain.Entities;

public sealed class GroupUser
{
    public long Id { get; set; }

    public string? GroupCode { get; set; }

    public string? UserCode { get; set; }

    public Group? Group { get; set; }

    public User? User { get; set; }
}
