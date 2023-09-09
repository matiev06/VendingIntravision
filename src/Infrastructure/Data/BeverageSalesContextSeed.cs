using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;
using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;

namespace VendingIntravision.Infrastructure.Data;

public class BeverageSalesContextSeed
{
    public static async Task SeedAsync(BeverageSalesContext beverageSalesContext, ILogger logger, int retry = 0)
    {
        var retryForAvailablity = retry;
        try
        {
            if (beverageSalesContext.Database.IsSqlServer())
            {
                beverageSalesContext.Database.Migrate();
            }

            if (!await beverageSalesContext.Beverages.AnyAsync())
            {
                await beverageSalesContext.Beverages.AddRangeAsync(
                    GetPreconfiguredBeverages());

                await beverageSalesContext.SaveChangesAsync();
            }

            if (!await beverageSalesContext.CoinInventories.AnyAsync())
            {
                await beverageSalesContext.CoinInventories.AddRangeAsync(
                    GetPreconfiguredCoinInventories());

                await beverageSalesContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailablity >= 10) throw;

            retryForAvailablity++;

            logger.LogError(ex.Message);
            await SeedAsync(beverageSalesContext, logger, retryForAvailablity);
            throw;
        }
    }

    static IEnumerable<Beverage> GetPreconfiguredBeverages()
    {
        return new List<Beverage>
        {
            new (Name: "Кола", ImageUrl: "/Images/cola.jpg", Price: 150, Quantity: 100),
            new (Name: "Вода", ImageUrl: "/Images/water.jpg", Price: 100, Quantity: 200),
            new (Name: "Апельсиновый Сок", ImageUrl: "/Images/orange-juice.jpg", Price: 200, Quantity: 105),
            new (Name: "Пепси", ImageUrl: "/Images/pepsi.jpg", Price: 150, Quantity: 102),
            new (Name: "Фанта", ImageUrl: "/Images/Fanta.jpg", Price: 120, Quantity: 108)
        };
    }

    static IEnumerable<CoinInventory> GetPreconfiguredCoinInventories()
    {
        return new List<CoinInventory>
        {
            new (CoinValue: 1, Quantity: 100, IsBlocked: false ),
            new (CoinValue: 2, Quantity: 50, IsBlocked: false ),
            new (CoinValue: 5, Quantity: 30, IsBlocked: false ),
            new (CoinValue: 10, Quantity: 20, IsBlocked: false )
        };
    }

}
