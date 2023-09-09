
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface IPurchaseService
{
    Task<Beverage> PurchaseDrinkAsync(int idUser, int idBeverage, int balance, int quantity = 1);
    Task<Dictionary<int, int>> GetChangeAsync(int totalAmount);
    Task<bool> InsertCoinsAsync(int iduser, params int[] coinDenominations);
}