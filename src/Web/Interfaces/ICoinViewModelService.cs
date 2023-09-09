using VendingIntravision.Web.ViewModel.Account;
using VendingIntravision.Web.ViewModel.Coin;

namespace VendingIntravision.Web.Interfaces;

public interface ICoinViewModelService
{
    Task UpdateStatusAQuantity(List<CoinViewModel> model);
    Task<List<CoinViewModel>> GetAllCoins();
    Task<object> GetResultOfChange(int userID);
}
