
using VendingIntravision.ApplicationCore.Interfaces;

namespace VendingIntravision.ApplicationCore.Services;

public class PurchaseService : IPurchaseService
{
    private readonly ICoinInventoryService _coinInventoryService;
    public PurchaseService(ICoinInventoryService coinInventoryService)
    {
        _coinInventoryService = coinInventoryService;
    }

    public Task<decimal> CalculateChangeAsync(decimal totalAmount)
    {
        var change = new Dictionary<decimal, decimal>();

        foreach (var item in _coinInventoryService.ValidCoinsDenomination().OrderByDescending(c => c))
        {
            decimal nCoin = totalAmount / item;
            if (nCoin > 0)
        }
    }

    public Task<PurchaseResult> PurchaseDrinkAsync(int idBeverage, List<int> coindIds)
    {
        throw new NotImplementedException();
    }
}
