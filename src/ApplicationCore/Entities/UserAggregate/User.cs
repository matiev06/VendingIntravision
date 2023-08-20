

namespace VendingIntravision.ApplicationCore.Entities.UserAggregate;

public class User : BaseEntity
{
    private User()
    {
        // required by EF
    }

    public User(int idIdentity, decimal balance)
    {
        IdIdentity = idIdentity;
        Balance = balance;
    }

    public int IdIdentity { get; protected set; }

    public decimal Balance { get; protected set; }

}
