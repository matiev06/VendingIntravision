using Ardalis.GuardClauses;
using VendingIntravision.ApplicationCore.Entities.UserAggregate;
using VendingIntravision.ApplicationCore.Extensions;
using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.ApplicationCore.Specefications;

namespace VendingIntravision.ApplicationCore.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }



    public async Task<User> GetUserByGuid(string guidUser)
    {
        var userSpec = new UserGuidSpecification(guidUser);
        var user = await _userRepository.GetBySpecAsync(userSpec);

        Guard.Against.NullUser(guidUser, user);

        return user;
    }

    public async Task<User> IsGetUserByGuid(string guidUser)
    {
        var userSpec = new UserGuidSpecification(guidUser);
        var user = await _userRepository.GetBySpecAsync(userSpec);


        return user;
    }

    public async Task<User> AddUserAsync(int idIdentity, int balance, string guidUser)
    {
        Guard.Against.NegativeOrZero(idIdentity, nameof(idIdentity));
        Guard.Against.Negative(balance, nameof(balance));

        var user = new User(idIdentity, balance, guidUser);

        await _userRepository.AddAsync(user);

        return user;
    }

    public async Task ChangeBalanceUser(int id, int balance)
    {
        Guard.Against.Negative(balance, nameof(balance));
        var user = await GetByIdAsync(id);

        user.SetBalance(balance);

        await _userRepository.UpdateAsync(user);
    }

    public async Task AddBalanceUser(int id, int balance)
    {
        Guard.Against.Negative(balance, nameof(balance));
        var user = await GetByIdAsync(id);

        int newBalance = user.Balance + balance;
        user.SetBalance(newBalance);

        await _userRepository.UpdateAsync(user);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        Guard.Against.NullUser(id, user);

        return user;
    }
}
