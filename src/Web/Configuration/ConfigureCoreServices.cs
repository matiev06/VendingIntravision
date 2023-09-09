using VendingIntravision.Infrastructure.Data;
using VendingIntravision.ApplicationCore.Interfaces;
using VendingIntravision.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace VendingIntravision.Web.Configuration;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBeverageService, BeverageService>();
        services.AddScoped<ICoinInventoryService, CoinInventoryService>();
        services.AddScoped<IPurchaseService, PurchaseService>();

        return services;
    }
}
