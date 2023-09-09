using Ardalis.Specification;
using VendingIntravision.ApplicationCore.Entities.UserAggregate;

namespace VendingIntravision.ApplicationCore.Specefications;

public class UserGuidSpecification : Specification<User>
{
    public UserGuidSpecification(string GuidUser)
    {
        Query
            .Where(u => u.Guid == GuidUser);
    }
}
