using Microsoft.EntityFrameworkCore;
using PChat.Domain.Dto;
using PChat.Domain.Entities;
using PChat.Domain.Interface;
using PChat.Persistence.Context;

namespace PChat.Persistence.Repositories;

internal sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public Task<User?> GetByIdAsync(UserId userId)
    {
        return context.Users
            .SingleOrDefaultAsync(p => p.Id == userId.Value);
    }
    
    public void Remove(User user)
    {
        context.Users.Remove(user);
    }
}