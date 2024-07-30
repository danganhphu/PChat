using Microsoft.EntityFrameworkCore;
using PChat.Domain.Entities;

namespace PChat.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    public DbSet<Call> Calls { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<GroupCall> GroupCalls { get; set; }

    public DbSet<GroupUser> GroupUsers { get; set; }

    public DbSet<Message> Messages { get; set; }
}
