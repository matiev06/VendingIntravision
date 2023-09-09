using VendingIntravision.Infrastructure;
using VendingIntravision.Infrastructure.Data;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using VendingIntravision.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

Dependencies.ConfigureServices(builder.Configuration, builder.Services);



builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();
builder.Services.AddRazorPages();

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("ru-Ru")
    };

    options.DefaultRequestCulture = new RequestCulture("ru-Ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddHttpClient();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromDays(30);
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var beverageSalesontext = scopedProvider.GetRequiredService<BeverageSalesContext>();
        await BeverageSalesContextSeed.SeedAsync(beverageSalesontext, app.Logger);

    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Îרטבךא ןנט גûחמגו seeding the DB");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(app =>
{
    app.MapControllerRoute(
        name: "admin",
        pattern: "Admin/{action=Index}/{id?}",
        defaults: new { controller = "Admin" });

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllers();
});

app.Run();
