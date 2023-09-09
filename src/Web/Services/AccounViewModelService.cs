using Microsoft.AspNetCore.Http;
using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.Web.Interfaces;
using VendingIntravision.Web.ViewModel.Account;

namespace VendingIntravision.Web.Services;

public class AccounViewModelService : IAccountViewModelService
{

    private readonly IUserService _userService;

    public AccounViewModelService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string> CreateAccountToCookie(HttpRequest httpRequest, HttpResponse httpResponse)
    {

        string? userGuid = httpRequest.Cookies["userGuid"];
        if (!string.IsNullOrEmpty(userGuid))
        {
            return userGuid;
        }

        userGuid = $"{Guid.NewGuid()}_{Guid.NewGuid()}";
        CookieOptions options = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddDays(7),
            Path = "/",
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        };

        httpResponse.Cookies.Append("userGuid", userGuid, options);
        await _userService.AddUserAsync(1, 0, userGuid);
         

        return userGuid;
    }




    public async Task<AccountViewModel> GetUserAccountByGuid(string userGuid)
    {

        var user = await _userService.IsGetUserByGuid(userGuid);

        if (user == null)
        {
            user = await _userService.AddUserAsync(1, 0, userGuid);
        }

        var model = new AccountViewModel
        {
            Id = user.Id,
            Balance = user.Balance,
            IdIdentity = user.Id,
            GuidIDUser = user.Guid
        };

        return model;
    }
}
