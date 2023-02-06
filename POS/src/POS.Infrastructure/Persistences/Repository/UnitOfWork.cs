using POS.Infrastructure.Persistences.Context;
using POS.Infrastructure.Persistences.Interface;

namespace POS.Infrastructure.Persistences.Repository;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository User { get; private set; }
    private readonly FormContext _context;

    public UnitOfWork(FormContext context)
    {
        _context = context;
        User = new UserRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
