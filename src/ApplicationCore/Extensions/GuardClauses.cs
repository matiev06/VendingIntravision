using Ardalis.GuardClauses;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;
using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;
using VendingIntravision.ApplicationCore.Entities.UserAggregate;
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

public static class UserGuards
{
    public static void NullUser(this IGuardClause clause, int idUser, User user)
    {
        if (user is null)
            throw new CoinInventoryNotFoundExcetion(idUser);
    }

    public static void NullUser(this IGuardClause clause, string guidUser, User user)
    {
        if (user is null)
            throw new CoinInventoryNotFoundExcetion(guidUser);
    }
}
