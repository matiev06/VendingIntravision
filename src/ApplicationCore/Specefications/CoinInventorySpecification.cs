using Ardalis.Specification;
using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;

namespace VendingIntravision.ApplicationCore.Specefications;

public class CoinInventorySpecification: Specification<CoinInventory>
{
    public CoinInventorySpecification(int CoinValue)
    {
        Query
            .Where(c => c.CoinValue == CoinValue);
    }
}
