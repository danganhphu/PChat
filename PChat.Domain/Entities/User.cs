using Microsoft.AspNetCore.Identity;

namespace PChat.Domain.Entities;

public sealed class User : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string Dob { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public string? Gender { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? CurrentSession { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpires { get; set; }

    public ICollection<Call> Calls { get; set; } = new List<Call>();
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    public ICollection<Contact> ContactUsers { get; set; } = new List<Contact>();
    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
