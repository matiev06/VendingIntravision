using Microsoft.AspNetCore.Http;
using VendingIntravision.Web.ViewModel.Account;

namespace VendingIntravision.Web.Interfaces;

public interface IAccountViewModelService
{
    Task<string> CreateAccountToCookie(HttpRequest httpRequest, HttpResponse httpResponse);
    Task<AccountViewModel> GetUserAccountByGuid(string userGuid);
}
