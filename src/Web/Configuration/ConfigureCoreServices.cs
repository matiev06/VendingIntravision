using VendingIntravision.ApplicationCore.Interfaces;

namespace VendingIntravision.Web.Configuration;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(IRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(IRepository<>));

        return services;
    }
}
