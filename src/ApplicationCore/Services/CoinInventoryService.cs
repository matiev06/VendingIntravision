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


    public CoinInventoryService(IRepository<CoinInventory> coinInventoryRepository)
    {
        _coinInventoryRepository = coinInventoryRepository;

    }

    public async Task<IReadOnlyDictionary<int, int>> IsHaveCoinsDenomination() 
    {
        var listCoins = await _coinInventoryRepository.ListAsync();

        return listCoins.ToDictionary(c => c.CoinValue, q => q.Quantity);
    }

    public async Task<List<CoinInventory>> GetAllCoinsDenomination()
    {
        var listCoins = await _coinInventoryRepository.ListAsync();

        return listCoins;
    }

    public async Task SetQuantityToCoinInventory(int id, int quantity)
    {
        var coinInventor = await GetByIdAsync(id);

        coinInventor.SetQuantity(quantity);

        await _coinInventoryRepository.UpdateAsync(coinInventor);
    }

    public async Task AddQuantityToCoinInventory(int CoinValue, int quantity)
    {
        var coinSpec = new CoinInventorySpecification(CoinValue);
        var coin = await _coinInventoryRepository.GetBySpecAsync(coinSpec);

        Guard.Against.NullCoinInventory(CoinValue, coin);

        quantity += coin.Quantity;

        coin.SetQuantity(quantity);

        await _coinInventoryRepository.UpdateAsync(coin);
    }

    public async Task BlockedCoinById(int id)
    {
        var coinInventory = await GetByIdAsync(id);

        coinInventory.ChangeStatus(true);

        await _coinInventoryRepository.UpdateAsync(coinInventory);
    }

    public async Task SetStatus(int id, bool status)
    {
        var coinInventory = await GetByIdAsync(id);

        coinInventory.ChangeStatus(status);

        await _coinInventoryRepository.UpdateAsync(coinInventory);
    }

    public async Task UnblockedCoinById(int id)
    {
        var coinInventory = await GetByIdAsync(id);

        coinInventory.ChangeStatus(false);

        await _coinInventoryRepository.UpdateAsync(coinInventory);
    }

    public async Task<CoinInventory> GetByCoinAsync(int CoinValue)
    {
        var coinInventorySpec = new CoinInventorySpecification(CoinValue);
        var coinInventory = await _coinInventoryRepository.GetBySpecAsync(coinInventorySpec);

        Guard.Against.NullCoinInventory(CoinValue, coinInventory);

        return coinInventory;
    }

    public async Task<bool> IsBlockedCoinByCoin(int coinValue)
    {
        var coinInventorySpec = new CoinInventorySpecification(coinValue);
        var coinInventory = await _coinInventoryRepository.GetBySpecAsync(coinInventorySpec);

        Guard.Against.NullCoinInventory(coinValue, coinInventory);

        return coinInventory.IsBlocked;
    }

    public async Task<CoinInventory> GetByIdAsync(int id)
    {
        var coinInventory = await _coinInventoryRepository.GetByIdAsync(id);

        Guard.Against.NullCoinInventory(id, coinInventory);

        return coinInventory;
    }

    public async Task<CoinInventory> IsGetByIdAsync(int id)
    {
        var coinInventory = await _coinInventoryRepository.GetByIdAsync(id);

        return coinInventory;
    }

}
