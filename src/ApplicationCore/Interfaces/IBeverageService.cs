
namespace VendingIntravision.ApplicationCore.Interfaces;

public interface IBeverageService
{
    Task<List<int>> GetValidDrinksAsync();
    Task AddItemToBeveregeAsync(string Name, string ImageUrl, decimal Price, int Quantity);
    Task SetQuantitiesAsync(int id, int Quantity);
    Task SetPriceAsync(int id, decimal Price);
    Task ChangeOrSetDataAsync(int id, string Name, string ImageUrl, decimal Price, int Quantity);
    Task DeleteItemBeveregeAsync(int id);
}
