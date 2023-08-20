using Ardalis.GuardClauses;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;
using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;
using VendingIntravision.ApplicationCore.Exceptions;

namespace VendingIntravision.ApplicationCore.Extensions;

public static class BeverageGuards
{
    public static void NullBeverage(this IGuardClause clause, int idbeverage, Beverage beverage)
    {
        if (beverage is null)
            throw new BeverageNotFoundExcetion(idbeverage);
    }
}

public static class CoinInventoryGuards
{
    public static void NullCoinInventory(this IGuardClause clause, int idCoin, CoinInventory coin)
    {
        if (coin is null)
            throw new CoinInventoryNotFoundExcetion(idCoin);
    }
}
