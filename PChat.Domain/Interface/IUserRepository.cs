using PChat.Domain.Dto;
using PChat.Domain.Entities;

namespace PChat.Domain.Interface;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
    void Remove(User user);
}