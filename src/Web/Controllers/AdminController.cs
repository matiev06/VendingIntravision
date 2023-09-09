using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using VendingIntravision.Web.Interfaces;
using VendingIntravision.Web.ViewModel.Admin;

namespace VendingIntravision.Web.Controllers;

[Route("[controller]")]
public class AdminController : Controller
{
    private readonly string secretKey = "MySecretKey";

    private readonly IBeverageViewModelService _beverageViewModelService;
    private readonly ICoinViewModelService _coinViewModelService;
    private readonly IUrlHelperFactory _urlHelperFactory;

    public AdminController(IBeverageViewModelService beverageViewModelService, ICoinViewModelService coinViewModelService,
        IUrlHelperFactory urlHelperFactory)
    {
        _beverageViewModelService = beverageViewModelService;
        _urlHelperFactory = urlHelperFactory;
        _coinViewModelService = coinViewModelService;
    }

    public async Task<IActionResult> Index(string? key)
    {
        if (key != secretKey)
        {
            return Unauthorized();
        }
        AdminViewModel vmAdmin = new();

        vmAdmin.Beverages = await _beverageViewModelService.GetAllBeverages();

        vmAdmin.Beverages.Add(new()
        {
            Name = "Name"
        });

        vmAdmin.Coins = await _coinViewModelService.GetAllCoins();


        return View(vmAdmin);
    }

    [HttpPost]
    public async Task<IActionResult> Index(AdminViewModel model)
    {

        var urlHelper = _urlHelperFactory.GetUrlHelper(ControllerContext);
        var url = urlHelper.Action(nameof(HomeController.Index), "Home", null, Request.Scheme);
        return Redirect(url);
    }


    [HttpPost]
    [Route("managebeverages")]
    public async Task<IActionResult> ManageBeverages(AdminViewModel model, string? key)
    {
        if (key != secretKey)
        {
            return Unauthorized();
        }

        model.Beverages = await _beverageViewModelService.CreateImageToWebHost(HttpContext.Request.Form.Files, model.Beverages);

        await _beverageViewModelService.UpdateOrCreateDrinks(model.Beverages);

        await _coinViewModelService.UpdateStatusAQuantity(model.Coins);

        return RedirectToAction("Index", new { key });
    }


    [HttpPost("removebeverage")]
    public async Task<IActionResult> RemoveBeverage([FromBody] RemoveBeverageViewModel model, string? key)
    {
        if (key != secretKey)
        {
            return Unauthorized();
        }

        await _beverageViewModelService.RemImageFromWebHost(new () { model.Beverage });

        await _beverageViewModelService.RemBeverages(new() { model.Beverage });

        return RedirectToAction("Index", new { key });
    }

}