

using Ardalis.GuardClauses;

namespace VendingIntravision.ApplicationCore.Entities.BeverageAggregate;

public class Beverage: BaseEntity
{
    private Beverage()
    {
        // reguired by EF
    }

    public Beverage(string Name, string ImageUrl, decimal Price, int Quantity)
    {
        this.Name = Name;
        this.ImageUrl = ImageUrl;
        this.Price = Price;
        this.Quantity = Quantity;
    }

    public void UpdateDetails(string name, string imageUrl, decimal price, int quantity)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(imageUrl, nameof(imageUrl));
        Guard.Against.Negative(price, nameof(price));
        //throw new DataBeverageItemNullException($"Data Name {Name} or ImageUtl {ImageUrl} is null");
        if (quantity <= 0)
            quantity = 1;

        this.Name = name;
        this.ImageUrl = imageUrl;
        this.Price = price;
        this.Quantity = quantity;
    }

    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

}
