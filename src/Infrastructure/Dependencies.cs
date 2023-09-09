using VendingIntravision.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Security;

namespace VendingIntravision.Infrastructure;

public class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<BeverageSalesContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("BeverageSalesConnection")));
    }
}