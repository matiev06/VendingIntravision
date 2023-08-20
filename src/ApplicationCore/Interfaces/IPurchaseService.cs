

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface IPurchaseService
{
    Task<PurchaseResult> PurchaseDrinkAsync(int idBeverage, List<int> coindIds);
    Task<decimal> CalculateChangeAsync(decimal totalAmount);
}


public enum PurchaseResult
{
    Success,
    InsufficientFunds,
    BeverageOutOfStock,
    InvalidCoins,
    OtherError
}
