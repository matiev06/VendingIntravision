using VendingIntravision.Web.ViewModel.Beverage;
using VendingIntravision.Web.ViewModel.Coin;

namespace VendingIntravision.Web.ViewModel.Admin;

public class AdminViewModel
{
    public List<BeverageViewModel> Beverages { get; set; }
    public List<CoinViewModel> Coins { get; set; }
    public BeverageViewModel RemBeverage { get; set; }

}
