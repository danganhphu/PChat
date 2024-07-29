using PChat.Domain.Entities;

namespace PChat.Domain.Interfaces;

public interface ICallRepository : IGenericRepository<Call>
{
    Task JoinVideoCall(string userSession, string url);
}