using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Context;
using POS.Infrastructure.Persistences.Interface;
using POS.Utilities.Static;

namespace POS.Infrastructure.Persistences.Repository;

public class UserRepository : IUserRepository
{
    private readonly FormContext _context;

    public UserRepository(FormContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> LIstSelectUser()
    {
        IEnumerable<User> user = await _context.User
            .Where(x => x.Status.Equals((int)StateType.Activo))
            .AsNoTracking()
            .ToListAsync();
        return user;
    }

    public async Task<User> AccountUserName(string userName)
    {
        User? user = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.Equals(userName));
        return user!;
    }

    public async Task<User> UserById(int userId)
    {
        User? user = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId.Equals(userId));
        return user!;
    }

    public async Task<bool> RegisterUser(User userRequest)
    {
        bool user = await _context.User.Where(x => x.Email.Equals(userRequest.Email)).AnyAsync();
        if (user)
            return false;
        await _context.User.AddAsync(userRequest);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUser(User userRequest)
    {
        _context.User.Update(userRequest);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUser(int userId)
    {
        User? user = await UserById(userId);
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
