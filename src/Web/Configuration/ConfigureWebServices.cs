using VendingIntravision.Web.Interfaces;
using VendingIntravision.Web.Services;

namespace VendingIntravision.Web.Configuration;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBeverageViewModelService, BeverageViewModelService>();
        services.AddScoped<ICoinViewModelService, CoinViewModelService>();
        services.AddScoped<IAccountViewModelService, AccounViewModelService>();


        return services;
    }
}
