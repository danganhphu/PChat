namespace PChat.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICallRepository CallRepository { get; }
    int Complete();
}