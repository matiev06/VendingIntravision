using Ardalis.GuardClauses;
using System.Runtime.InteropServices;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;
using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;
using VendingIntravision.ApplicationCore.Exceptions;
using VendingIntravision.ApplicationCore.Extensions;
using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.ApplicationCore.Specefications;

namespace VendingIntravision.ApplicationCore.Services;

public class CoinInventoryService : ICoinInventoryService
{

    private readonly IRepository<CoinInventory> _coinInventoryRepository;

    private readonly List<int> _validCoinsDenomination /*= new List<int>
        {
            1, 2, 5, 10 denominationCoin
        }*/;

    public CoinInventoryService(IRepository<CoinInventory> coinInventoryRepository)
    {
        _coinInventoryRepository = coinInventoryRepository;

        _validCoinsDenomination = new List<int>
        {
            1, 2, 5, 10
        };

    }

    public IReadOnlyDictionary<int, int> ValidCoinsDenomination() 
    {
        return 
    }

    public async Task SetQuantityToCoinInventory(int id, int quantity)
    {
        var coinInventor = await _coinInventoryRepository.GetByIdAsync(id);
        Guard.Against.NullCoinInventory(id, coinInventor);

        coinInventor.SetQuantity(quantity);

        await _coinInventoryRepository.UpdateAsync(coinInventor);
    }

    public async Task AddQuantityToCoinInventory(int id, int quantity)
    {
        var coin = await _coinInventoryRepository.GetByIdAsync(id);
        //if (!_validCoinsDenomination.Contains(id))
        //    throw new NotValidCoinException($"Монета {id} недоступна.");
        Guard.Against.NullCoinInventory(id, coin);

        quantity += coin.Quantity;

        coin.SetQuantity(quantity);

        await _coinInventoryRepository.UpdateAsync(coin);
    }

    public async Task BlockedCoinById(int id)
    {
        var coinInventory = await _coinInventoryRepository.GetByIdAsync(id);

        Guard.Against.NullCoinInventory(id, coinInventory);

        coinInventory.ChangeStatus(true);

        await _coinInventoryRepository.UpdateAsync(coinInventory);
    }

    public async Task UnblockedCoinById(int id)
    {
        var coinInventory = await _coinInventoryRepository.GetByIdAsync(id);

        Guard.Against.NullCoinInventory(id, coinInventory);

        coinInventory.ChangeStatus(false);

        await _coinInventoryRepository.UpdateAsync(coinInventory);
    }

    public async Task<int> GetIDByCoin(int CoinValue)
    {
        var coinInventorySpec = new CoinInventorySpecification(CoinValue);
        var coinInventory = await _coinInventoryRepository.GetBySpecAsync(coinInventorySpec);

        Guard.Against.NullCoinInventory(CoinValue, coinInventory);

        return coinInventory.Id;
    }

}
