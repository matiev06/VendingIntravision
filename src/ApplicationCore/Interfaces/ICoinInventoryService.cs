

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface ICoinInventoryService
{
    public IReadOnlyDictionary<int, int> ValidCoinsDenomination();
    Task SetQuantityToCoinInventory(int id, int quantity);
    Task AddQuantityToCoinInventory(int id, int quantity);
    Task<int> GetIDByCoin(int CoinValue);
    Task BlockedCoinById(int id);
    Task UnblockedCoinById(int id);
}
