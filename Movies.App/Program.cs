using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.AppServices.ReaderAppService;
using Movies.AppServices.ReaderAppService.Interface;
using Movies.AppServices.RecentlyViewedAppService;
using Movies.AppServices.RecentlyViewedAppService.Inteface;
using Movies.AppServices.ShowAppService;
using Movies.AppServices.ShowAppService.Interface;
using Movies.Data;
using Movies.Models.Models;
using Movies.Models.Models.Identity;

namespace Movies.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        RegisterServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        await ConfigureApp(app);

        await app.RunAsync();
    }

    private static async Task ConfigureApp(WebApplication app)
    {
        await SeedDatabase(app.Services);

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSession();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
    }

    private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>();

        services.AddSession();
        services.AddHttpContextAccessor();
        services.AddControllersWithViews();
        
        services.AddScoped<IReaderAppService, ReaderAppService>();
        services.AddTransient<IShowAppService, ShowAppService>();
        services.AddTransient<IRecentlyViewedAppService, RecentlyViewedAppService>();
        
        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount =
                    configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
                options.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("Identity:RequireConfirmedEmail");
                options.SignIn.RequireConfirmedPhoneNumber =
                    configuration.GetValue<bool>("Identity:RequireConfirmedPhoneNumber");
                options.Password.RequireLowercase = configuration.GetValue<bool>("Identity:RequireLowercase");
                options.Password.RequireUppercase = configuration.GetValue<bool>("Identity:RequireUppercase");
                options.Password.RequireNonAlphanumeric =
                    configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
                options.Password.RequiredLength = configuration.GetValue<int>("Identity:RequiredLength");
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
        });

        services.AddRazorPages();
    }

    private static async Task SeedDatabase(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var readerService = scope.ServiceProvider.GetRequiredService<IReaderAppService>();
        await readerService.SeedDatabaseAsync();
    }
}