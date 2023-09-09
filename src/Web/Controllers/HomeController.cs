using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using VendingIntravision.Web.Interfaces;
using VendingIntravision.Web.ViewModel.Home;

namespace VendingIntravision.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBeverageViewModelService _beverageViewModelService;
    private readonly ICoinViewModelService _coinViewModelService;
    private readonly IAccountViewModelService _accountViewModelService;
    private readonly IUrlHelperFactory _urlHelperFactory;

    private readonly string secretKey = "MySecretKey";

    public HomeController(ILogger<HomeController> logger, IBeverageViewModelService beverageViewModelService,
        ICoinViewModelService coinViewModelService, IAccountViewModelService accountViewModelService,
        IUrlHelperFactory urlHelperFactory)
    {
        _logger = logger;
        _coinViewModelService = coinViewModelService;
        _accountViewModelService = accountViewModelService;
        _urlHelperFactory = urlHelperFactory;
        _beverageViewModelService = beverageViewModelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {

        string? userGuid = await _accountViewModelService.CreateAccountToCookie(Request, Response);

        HomeViewModel vmHome = new();

        vmHome.Beverages = await _beverageViewModelService.GetAllBeverages();

        vmHome.Coins = await _coinViewModelService.GetAllCoins();


        vmHome.User = await _accountViewModelService.GetUserAccountByGuid(userGuid);



        return View(vmHome);
    }

    [HttpPost]
    public async Task<IActionResult> Index(HomeViewModel model)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(ControllerContext);
        var url = urlHelper.Action(nameof(AdminController.Index), "Admin", new { key = secretKey }, Request.Scheme);
        return Redirect(url);
    }



    [HttpGet("GetChange/{idUser}")]
    public async Task<IActionResult> GetChange(int idUser)
    {
        var answer = await _coinViewModelService.GetResultOfChange(idUser);

        return new ObjectResult(answer) { StatusCode = StatusCodes.Status201Created };
    }


}