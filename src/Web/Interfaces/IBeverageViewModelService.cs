using VendingIntravision.Web.ViewModel.Beverage;

namespace VendingIntravision.Web.Interfaces;

public interface IBeverageViewModelService
{
    Task<List<BeverageViewModel>> CreateImageToWebHost(IFormFileCollection Files, List<BeverageViewModel> model);
    Task<bool> RemImageFromWebHost(List<BeverageViewModel> models);
    Task<bool> RemBeverages(List<BeverageViewModel> models);
    Task UpdateOrCreateDrinks(List<BeverageViewModel> model);
    Task<PurchaseResult> PurchaseBeverage(int userID, List<BeverageViewModel> models);
    Task<BeverageViewModel> GetBeverageById(int id);
    Task<List<BeverageViewModel>> GetAllBeverages();
}



public enum PurchaseResult
{
    Success, // Успех
    InsufficientFunds, // Недостаточно средств
    BeverageOutOfStock, // Напитков нет на складе
    CoinIsBlocked, // Напитков нет на складе
    OtherError // Другая ошибка
}
