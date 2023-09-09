

namespace VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;

public class CoinInventory : BaseEntity
{
    private CoinInventory()
    {
        // required by EF
    }

    public CoinInventory(int CoinValue, int Quantity, bool IsBlocked)
    {
        this.CoinValue = CoinValue;
        this.Quantity = Quantity;
        this.IsBlocked = IsBlocked;
    }

    public int CoinValue { get; protected set; }
    public int Quantity { get; protected set; }
    public bool IsBlocked { get; protected set; }

    public void SetQuantity(int Quantity)
    {
        this.Quantity = Quantity;
    }

    public void ChangeStatus(bool blockCoin)
    {
        IsBlocked = blockCoin;
    }


}
