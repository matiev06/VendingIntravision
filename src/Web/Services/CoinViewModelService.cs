using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.Web.Interfaces;
using VendingIntravision.Web.ViewModel.Coin;

namespace VendingIntravision.Web.Services;

public class CoinViewModelService : ICoinViewModelService
{
    private readonly ICoinInventoryService _coinInventoryService;
    private readonly IUserService _userService;
    private readonly IPurchaseService _purchaseService;

    public CoinViewModelService(ICoinInventoryService coinInventoryService, IUserService userService,
        IPurchaseService purchaseService)
    {
        _coinInventoryService = coinInventoryService;
        _userService = userService;
        _purchaseService = purchaseService;
    }

    public async Task<List<CoinViewModel>> GetAllCoins()
    {
        var coins = await _coinInventoryService.GetAllCoinsDenomination();
        var model = coins.Select(c => new CoinViewModel
        {
            Id = c.Id,
            CoinValue = c.CoinValue,
            Quantity = c.Quantity,
            IsBlocked = c.IsBlocked
        }).ToList();

        return model;
    }

    public async Task<object> GetResultOfChange(int userID)
    {
        var userData = await _userService.GetByIdAsync(userID);
        var changeDic = await _purchaseService.GetChangeAsync(userData.Balance);

        int sumchange = 0;
        string changeText = $"Вы получили сдачу\n\t";
        foreach (var item in changeDic)
        {
            sumchange += item.Key * item.Value;
            changeText += $"{item.Value} монет по {item.Key} рублю\n\t";
        }

        int balance = userData.Balance - sumchange;

        await _userService.ChangeBalanceUser(userID, balance);

        return new { balance, changeText };

    }

    public async Task UpdateStatusAQuantity(List<CoinViewModel> model)
    {
        foreach (var coin in model)
        {
            await _coinInventoryService.SetStatus(coin.Id, coin.IsBlocked);
            await _coinInventoryService.SetQuantityToCoinInventory(coin.Id, coin.Quantity);
        }
    }
}
