using VendingIntravision.ApplicationCore.Exceptions;
using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.Web.Interfaces;
using VendingIntravision.Web.ViewModel.Beverage;

namespace VendingIntravision.Web.Services;

public class BeverageViewModelService : IBeverageViewModelService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<BeverageViewModelService> _logger;
    private readonly IBeverageService _beverageService;
    private readonly IUserService _userService;
    private readonly IPurchaseService _purchaseService;

    public BeverageViewModelService(IWebHostEnvironment webHostEnvironment, ILogger<BeverageViewModelService> logger,
        IBeverageService beverageService, IUserService userService,
        IPurchaseService purchaseService)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _beverageService = beverageService;
        _userService = userService;
        _purchaseService = purchaseService;
    }

    public async Task<List<BeverageViewModel>> CreateImageToWebHost(IFormFileCollection Files, List<BeverageViewModel> model)
    {

        foreach (var file in Files)
        {
            if (file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);


                var imgmodel = model.First(c => c.ImageFile != null && c.ImageFile.FileName == file.FileName);
                imgmodel.ImageUrl = $"/Images/{uniqueFileName}";


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }


        return model;
    }

    public async Task<bool> RemImageFromWebHost(List<BeverageViewModel> models)
    {

        foreach (var model in models)
        {
            var beverage = await _beverageService.GetByIdAsync(model.Id);

            string uniqueFileName = beverage.ImageUrl.Replace("/Images/", string.Empty);
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, uniqueFileName);



            if (!File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);

                fileInfo.Delete();
            }
        }

        return true;
    }

    public async Task<bool> RemBeverages(List<BeverageViewModel> models)
    {
        foreach (var model in models)
        {
            await _beverageService.DeleteItemBeveregeAsync(model.Id);
        }

        return true;
    }

    public async Task<List<BeverageViewModel>> GetAllBeverages()
    {
        var beverages = await _beverageService.GetAllBeverageAsync();
        var models = beverages.Select(b => new BeverageViewModel
        {
            Id = b.Id,
            Name = b.Name,
            ImageUrl = b.ImageUrl,
            Price = b.Price,
            Quantity = b.Quantity
        }).ToList();


        return models;
    }



    public async Task UpdateOrCreateDrinks(List<BeverageViewModel> model)
    {

        foreach (var vmbeverage in model)
        {
            var beverage = await _beverageService.IsGetByIdAsync(vmbeverage.Id);
            if (beverage != null)
            {
                if (vmbeverage.ImageUrl == null)
                {
                    vmbeverage.ImageUrl = beverage.ImageUrl;
                }

                await _beverageService.ChangeOrSetDataAsync(vmbeverage.Id, vmbeverage.Name, vmbeverage.ImageUrl, vmbeverage.Price, vmbeverage.Quantity);
            }
            else if (vmbeverage.Price != 0 || vmbeverage.Quantity != 0 || !string.IsNullOrEmpty(vmbeverage.ImageUrl))
            {
                if (string.IsNullOrEmpty(vmbeverage.ImageUrl))
                {
                    vmbeverage.ImageUrl = "/Images/upload-file.svg";
                }

                await _beverageService.AddItemToBeveregeAsync(vmbeverage.Name, vmbeverage.ImageUrl, vmbeverage.Price, vmbeverage.Quantity);
            }

        }
    }

    public async Task<PurchaseResult> PurchaseBeverage(int userID, List<BeverageViewModel> models)
    {
        var userData = await _userService.GetByIdAsync(userID);
        foreach (var beverage in models)
        {
            try
            {
                var addedBeverage = await _purchaseService.PurchaseDrinkAsync(userID, beverage.Id, userData.Balance, beverage.Quantity);
                userData = await _userService.GetByIdAsync(userID);
            }
            catch (InsufficientFundsException ex) // Недостаточно средств
            {
                return PurchaseResult.InsufficientFunds;
            }
            catch (BeverageOutOfStockException ex) // Напитков нет на складе
            {
                return PurchaseResult.BeverageOutOfStock;
            }


        }


        return PurchaseResult.Success;
    }

    public async Task<BeverageViewModel> GetBeverageById(int id)
    {
        var beverage = await _beverageService.IsGetByIdAsync(id);


        if (beverage == null)
        {
            return null;
        }

        var model = new BeverageViewModel
        {
            Id = beverage.Id,
            Name = beverage.Name,
            ImageUrl = beverage.ImageUrl,
            Price = beverage.Price,
            Quantity = beverage.Quantity,
        };

        return model;
    }
}
