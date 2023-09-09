using Microsoft.AspNetCore.Mvc;
using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.Web.Interfaces;
using VendingIntravision.Web.ViewModel.Beverage;

namespace VendingIntravision.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeveragesController : Controller
{

    private readonly IBeverageViewModelService _beverageViewModelService;
    private readonly IBeverageService _beverageService;

    public BeveragesController(IBeverageViewModelService beverageViewModelService, IBeverageService beverageService)
    {
        _beverageViewModelService = beverageViewModelService;
        _beverageService = beverageService;
    }



    [HttpGet("GetAllBeverages")]
    public async Task<IActionResult> GetAllBeverages()
    {
        var vm = await _beverageViewModelService.GetAllBeverages();

        return Ok(vm);
    }

    [HttpPost("AddBeverage")]
    public async Task<IActionResult> AddBeverage([FromBody] BeverageViewModel vm)
    {
        var addedBeverage = await _beverageService.AddItemToBeveregeAsync(vm.Name, vm.ImageUrl, vm.Price, vm.Quantity);

        return new ObjectResult(new { status = 201, id = addedBeverage.Id }) { StatusCode = StatusCodes.Status201Created };
    }


    [HttpPost("PurchaseBeverage")]
    public async Task<IActionResult> PurchaseBeverage([FromBody] PurchaseViewModel vm)
    {
        if (vm.CartItems.Count <= 0)
        {
            return new ObjectResult(new { message = "В корзине нет напитков" }){ StatusCode = StatusCodes.Status204NoContent };
        }

        PurchaseResult result = await _beverageViewModelService.PurchaseBeverage(vm.IdUser, vm.CartItems);

        string message = string.Empty;
        if (result == PurchaseResult.Success)
        {
            message = "Напитки успешно купленны.";
        }

        return new ObjectResult(new { status = 201, message }) { StatusCode = StatusCodes.Status201Created };
    }


    [HttpGet("GetBeverageById/{id}")]
    public async Task<IActionResult> GetBeverageById(int id)
    {
        BeverageViewModel mv = await _beverageViewModelService.GetBeverageById(id);


        if (mv == null)
        {
            return NotFound(new { message = "К сожалению, такого напитка у нас еще нет." });
        }

        return Ok(mv);
    }


}
