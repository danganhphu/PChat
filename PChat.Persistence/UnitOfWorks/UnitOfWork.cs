using PChat.Domain.Interfaces;
using PChat.Persistence.Context;
using PChat.Persistence.Repositories;

namespace PChat.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        CallRepository = new CallRepository(_context);
    }

    public ICallRepository CallRepository { get; private set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}