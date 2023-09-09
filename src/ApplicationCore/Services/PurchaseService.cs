
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;
using VendingIntravision.ApplicationCore.Exceptions;
using VendingIntravision.ApplicationCore.Interfaces;

namespace VendingIntravision.ApplicationCore.Services;

public class PurchaseService : IPurchaseService
{
    private readonly ICoinInventoryService _coinInventoryService;
    private readonly IBeverageService _beverageService;
    private readonly IUserService _userService;

    public PurchaseService(ICoinInventoryService coinInventoryService,
        IBeverageService beverageService, IUserService userService)
    {
        _coinInventoryService = coinInventoryService;
        _beverageService = beverageService;
        _userService = userService;
    }

    public async Task<Beverage> PurchaseDrinkAsync(int idUser, int idBeverage, int balance, int quantity = 1)
    {
        var listBeverage = await _beverageService.GetAllBeverageAsync();

        var beverage = listBeverage.FirstOrDefault(b => b.Id == idBeverage);

        if (beverage == null || beverage.Quantity < quantity)
            throw new BeverageOutOfStockException($"Ошибка: BeverageOutOfStock");

        int price = beverage.Price * quantity;
        if (balance < price)
            throw new InsufficientFundsException($"Ошибка: InsufficientFunds");

        int newbalance = balance - price;
        await _userService.ChangeBalanceUser(idUser, newbalance);

        await _beverageService.SetQuantitiesAsync(idBeverage, (beverage.Quantity - quantity));


        return beverage;
    }

    public async Task<bool> InsertCoinsAsync(int iduser, params int[] coinDenominations)
    {

        foreach (int coin in coinDenominations)
        {
            var statusCoin = await _coinInventoryService.IsBlockedCoinByCoin(coin);
            if (statusCoin)
                return false; //PurchaseResult.CoinIsBlocked;

            await _coinInventoryService.AddQuantityToCoinInventory(coin, 1);
        }

        int sumCoins = coinDenominations.Sum();
        await _userService.AddBalanceUser(iduser, sumCoins);

        return true;
    }

    public async Task<Dictionary<int, int>> GetChangeAsync(int totalAmount)
    {
        if (totalAmount <= 0)
            return new Dictionary<int, int> { { 0, 0 } };

        var validCoins = _coinInventoryService.IsHaveCoinsDenomination().Result;

        var change = CalculateChange(totalAmount, validCoins);

        foreach (var item in change)
        {
            var coinInventory = await _coinInventoryService.GetByCoinAsync(item.Key);

            int newquantity = coinInventory.Quantity - item.Value;

            await _coinInventoryService.SetQuantityToCoinInventory(coinInventory.Id, newquantity);
        }

        return change;
    }

    public Dictionary<int, int> CalculateChange(int totalAmount, IReadOnlyDictionary<int, int> validCoins)
    {
        var change = new Dictionary<int, int>();

        foreach (var item in validCoins.OrderByDescending(x => x.Key))
        {
            var coinQuantity = item.Value;
            var coinDenomination = item.Key;
            int nCoin = totalAmount / coinDenomination;
            if (nCoin > 0)
            {
                if (coinQuantity < nCoin)
                    nCoin = coinQuantity;
                change.Add(coinDenomination, nCoin);
                totalAmount -= coinDenomination * nCoin;
            }
            if (totalAmount <= 0)
                break;
        }

        return change;
    }
}
