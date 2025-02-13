using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.AppServices.RecentlyViewedAppService.Inteface;
using Movies.AppServices.ShowAppService.Interface;
using Movies.Dtos.Show;

namespace Movies.App.Controllers;

public class ShowController : Controller
{
    private const int ItemsPerPage = 6;
    
    private readonly IShowAppService _showService;
    private readonly IRecentlyViewedAppService _recentlyViewedService;
    
    public ShowController(
        IShowAppService showService, 
        IRecentlyViewedAppService recentlyViewedService)
    {
        _showService = showService;
        _recentlyViewedService = recentlyViewedService;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> All(int id = 1)
    {
        var model = new AllShowsDto()
        {
            ControllerName = "Show",
            ActionName = "All",
            ItemsPerPage = ItemsPerPage,
            PageNumber = id,
            EntityCount = await _showService.GetCountAsync(),
            Shows = await _showService.GetPagedAsync(id, ItemsPerPage)
        };
        
        return View(model);
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ById(string id)
    {
        var show = await this._showService.GetById(new Guid(id));
        
        _recentlyViewedService.SaveRecentlyViewed(new RecentlyViewedShowDto(show));

        return this.View(new ShowSingleDto(show));
    }
}