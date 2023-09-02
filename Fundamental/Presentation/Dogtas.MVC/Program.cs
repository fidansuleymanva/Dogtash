
using Fundamental.Application;
using Fundamental.Persistence;
using Fundamental.Infrastructure;
using Fundamental.Application.Enums;
using Fundamental.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Dogtas.MVC.Middlewares;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Dogtas.MVC.Localization.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Localization
//Step 1
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
        return factory.Create("ShareResource", assemblyName.Name);
    };
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo> {
        new CultureInfo("az"),
        new CultureInfo("ru"),
        new CultureInfo("en")
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "az", uiCulture: "az");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
#endregion

builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<MainContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"),
//                optionsBuilder => optionsBuilder.MigrationsAssembly("Dogtas.MVC"))
//               );

builder.Services.AddApplicationLayerServices();
builder.Services.AddPersistenceLayerServices();
builder.Services.AddInfrastructureLayerServices();
builder.Services.AddInfrastructureLayerServices(StorageEnum.LocalStorage);

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
 );
    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}"
   );

});

app.Run();
