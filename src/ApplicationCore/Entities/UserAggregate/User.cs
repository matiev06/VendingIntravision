

namespace VendingIntravision.ApplicationCore.Entities.UserAggregate;

public class User : BaseEntity
{
    private User()
    {
        // required by EF
    }

    public User(int idIdentity, int balance, string guid)
    {
        IdIdentity = idIdentity;
        Balance = balance;
        Guid = guid;
    }

    public int IdIdentity { get; protected set; }

    public int Balance { get; protected set; }
    public string Guid { get; protected set; }

    public void SetBalance(int balance)
    {
        Balance = balance;
    }

}
