using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.AppServices.ReaderAppService;
using Movies.AppServices.ReaderAppService.Interface;
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
        
        // using (var scope = app.Services.CreateScope())
        // {
        //     var readerService = scope.ServiceProvider.GetRequiredService<IReaderAppService>();
        //     await readerService.SeedDatabaseAsync();
        // }

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        await app.RunAsync();
    }

    private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>();

        services.AddControllersWithViews();
        
        services.AddScoped<IReaderAppService, ReaderAppService>();

        services.AddTransient<IShowAppService, ShowAppService>();
        
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

        services.AddRazorPages();
    }
}