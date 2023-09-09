using VendingIntravision.Web.ViewModel.Account;
using VendingIntravision.Web.ViewModel.Beverage;
using VendingIntravision.Web.ViewModel.Coin;

namespace VendingIntravision.Web.ViewModel.Home;

public class HomeViewModel
{
    public List<BeverageViewModel> Beverages { get; set; }
    public List<CoinViewModel> Coins { get; set; }
    public AccountViewModel User { get; set; }
}
