using Ardalis.Specification;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;

namespace VendingIntravision.ApplicationCore.Specefications;

public class BeverageSpecification : Specification<Beverage>
{
    public BeverageSpecification(int id)
    {
        Query
            .Where(c => c.Id == id);
    }
}
