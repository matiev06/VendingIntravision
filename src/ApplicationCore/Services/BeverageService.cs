using Ardalis.GuardClauses;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;
using VendingIntravision.ApplicationCore.Extensions;
using VendingIntravision.ApplicationCore.Interfaces;

namespace VendingIntravision.ApplicationCore.Services;

public class BeverageService : IBeverageService
{
    private readonly IRepository<Beverage> _beverageRepository;

    public BeverageService(IRepository<Beverage> beverageRepository)
    {
        _beverageRepository = beverageRepository;
    }

    public async Task<Beverage> AddItemToBeveregeAsync(string Name, string ImageUrl, int Price, int Quantity)
    {

        IsDataValid(Name, ImageUrl, Price, Quantity);

        var beverage = new Beverage(Name, ImageUrl, Price, Quantity);

        await _beverageRepository.AddAsync(beverage);

        return beverage;
    }

    public async Task ChangeOrSetDataAsync(int id, string Name, string ImageUrl, int Price, int Quantity)
    {
        var beverage = await GetByIdAsync(id);

        IsDataValid(Name, ImageUrl, Price, Quantity);

        beverage.UpdateDetails(Name, ImageUrl, Price, Quantity);

        await _beverageRepository.UpdateAsync(beverage);
    }

    public async Task DeleteItemBeveregeAsync(int id)
    {
        var beverage = await GetByIdAsync(id);
        await _beverageRepository.DeleteAsync(beverage);
    }

    public async Task SetPriceAsync(int id, int Price)
    {
        Guard.Against.Negative(Price, nameof(Price));

        var beverage = await GetByIdAsync(id);

        beverage.UpdateDetails(beverage.Name, beverage.ImageUrl, Price, beverage.Quantity);

        await _beverageRepository.UpdateAsync(beverage);
    }

    public async Task SetQuantitiesAsync(int id, int Quantity)
    {
        Guard.Against.Negative(Quantity, nameof(Quantity));

        var beverage = await GetByIdAsync(id);

        beverage.UpdateDetails(beverage.Name, beverage.ImageUrl, beverage.Price, Quantity);

        await _beverageRepository.UpdateAsync(beverage);
    }

    public async Task<List<int>> GetValidDrinksIdsAsync()
    {
        var listBeverage = await _beverageRepository.ListAsync();

        var validDrinks = listBeverage.Where(b => b.Quantity > 0).Select(b =>
        {
            return b.Id;
        }).ToList();

        return validDrinks;
    }
    

    public async Task<List<Beverage>> GetAllBeverageAsync()
    {
        var listBeverage = await _beverageRepository.ListAsync();

        return listBeverage;
    }


    private bool IsDataValid(string Name, string ImageUrl, decimal Price, int Quantity)
    {
        Guard.Against.NullOrEmpty(Name, nameof(Name));
        Guard.Against.NullOrEmpty(ImageUrl, nameof(ImageUrl));
        Guard.Against.Negative(Price, nameof(Price));

        if (Quantity <= 0)
            Quantity = 1;

        return true;
    }

    public async Task<Beverage> GetByIdAsync(int id)
    {
        var beverage = await _beverageRepository.GetByIdAsync(id);

        Guard.Against.NullBeverage(id, beverage);

        return beverage;
    }

    public async Task<Beverage> IsGetByIdAsync(int id)
    {
        var beverage = await _beverageRepository.GetByIdAsync(id);


        return beverage;
    }
}
