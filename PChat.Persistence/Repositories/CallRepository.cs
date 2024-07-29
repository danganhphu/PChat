using PChat.Domain.Entities;
using PChat.Domain.Interfaces;
using PChat.Persistence.Context;

namespace PChat.Persistence.Repositories;

public class CallRepository(AppDbContext context) : GenericRepository<Call>(context), ICallRepository
{
    public Task JoinVideoCall(string userSession, string url)
    {
        throw new NotImplementedException();
    }
}