using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface ICoinInventoryService
{
    //key - Номинал монет, value - Количество таких номиналов в автомате
    Task<IReadOnlyDictionary<int, int>> IsHaveCoinsDenomination();
    Task<List<CoinInventory>> GetAllCoinsDenomination();
    Task SetQuantityToCoinInventory(int id, int quantity);
    Task AddQuantityToCoinInventory(int CoinValue, int quantity);
    Task BlockedCoinById(int id);
    Task UnblockedCoinById(int id);
    Task SetStatus(int id, bool status);
    Task<bool> IsBlockedCoinByCoin(int coinValue);
    Task<CoinInventory> GetByCoinAsync(int CoinValue);
    Task<CoinInventory> GetByIdAsync(int id);
    Task<CoinInventory> IsGetByIdAsync(int id);
}
