using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Interface;

public interface IUserRepository
{
    Task<IEnumerable<User>> LIstSelectUser();
    Task<User> AccountUserName(string userName);
    Task<User> UserById(int userId);
    Task<bool> RegisterUser(User userRequest);
    Task<bool> UpdateUser(User userRequest);
    Task<bool> DeleteUser(int userId);
}
