
using VendingIntravision.ApplicationCore.Entities.UserAggregate;

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface IUserService
{
    Task<User> GetUserByGuid(string guidUser);
    Task<User> IsGetUserByGuid(string guidUser);
    Task<User> AddUserAsync(int idIdentity, int balance, string guidUser);
    Task ChangeBalanceUser(int id, int balance);
    Task AddBalanceUser(int id, int balance);
    Task<User> GetByIdAsync(int id);
}
