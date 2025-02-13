using Microsoft.AspNetCore.Mvc;
using Movies.AppServices.RecentlyViewedAppService.Inteface;
using Movies.AppServices.ShowAppService.Interface;
using Movies.Common.Enumerations;
using Movies.Dtos.Home;

namespace Movies.App.Controllers;

public class HomeController : Controller
{
    private readonly IShowAppService _showService;
    private readonly IRecentlyViewedAppService _recentlyViewedService;

    public HomeController(IRecentlyViewedAppService recentlyViewedService, IShowAppService showService)
    {
        _recentlyViewedService = recentlyViewedService;
        _showService = showService;
    }

    public async Task<IActionResult> Index()
    {
        var recentlyViewed = _recentlyViewedService.GetRecentlyViewed();
        var shortestMovies = await _showService.GetShortestByType(DurationType.Minutes);
        var shortestSeries = await _showService.GetShortestByType(DurationType.Seasons);
        
        return View(new HomeDto(recentlyViewed, shortestMovies, shortestSeries));
    }

    public IActionResult Privacy()
    {
        return View();
    }
}