using Microsoft.Extensions.FileProviders;

namespace VendingIntravision.Web.ViewModel.Beverage;

public class BeverageViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Price { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int Quantity { get; set; }
}
