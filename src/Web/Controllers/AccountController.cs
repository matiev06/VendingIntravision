using Microsoft.AspNetCore.Mvc;
using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.ApplicationCore.Services;
using VendingIntravision.Web.ViewModel.Account;
using VendingIntravision.Web.ViewModel.Beverage;

namespace VendingIntravision.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{

    private readonly IUserService _userService;
    private readonly ICoinInventoryService _coinInventoryService;

    public AccountController(IUserService userService, ICoinInventoryService coinInventoryService)
    {
        _userService = userService;
        _coinInventoryService = coinInventoryService;
    }


    [HttpPost("AddBalance")]
    public async Task<IActionResult> AddBalance([FromBody] AccountViewModel vm)
    {
        await _coinInventoryService.AddQuantityToCoinInventory(vm.Balance, 1);
        await _userService.AddBalanceUser(vm.Id, vm.Balance);

        return new ObjectResult(new { status = 201 }) { StatusCode = StatusCodes.Status201Created };
    }


}
