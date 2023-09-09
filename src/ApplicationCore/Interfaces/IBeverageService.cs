
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface IBeverageService
{
    Task<List<int>> GetValidDrinksIdsAsync();
    Task<List<Beverage>> GetAllBeverageAsync();
    Task<Beverage> AddItemToBeveregeAsync(string Name, string ImageUrl, int Price, int Quantity);
    Task SetQuantitiesAsync(int id, int Quantity);
    Task SetPriceAsync(int id, int Price);
    Task ChangeOrSetDataAsync(int id, string Name, string ImageUrl, int Price, int Quantity);
    Task DeleteItemBeveregeAsync(int id);
    Task<Beverage> GetByIdAsync(int id);
    Task<Beverage> IsGetByIdAsync(int id);
}
