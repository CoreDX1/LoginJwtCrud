namespace POS.Infrastructure.Persistences.Interface;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}
