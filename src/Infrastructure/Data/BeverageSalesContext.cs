using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;
using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;
using VendingIntravision.ApplicationCore.Entities.UserAggregate;

namespace VendingIntravision.Infrastructure.Data;

public class BeverageSalesContext : DbContext
{
    public BeverageSalesContext(DbContextOptions<BeverageSalesContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<CoinInventory> CoinInventories{ get; set; }
    public DbSet<Beverage> Beverages { get; set; }

    protected virtual void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Этот метод просматривает все классы в сборке, находит классы, которые реализуют интерфейс IEntityTypeConfiguration<T>, и применяет их конфигурации к модели данных.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
