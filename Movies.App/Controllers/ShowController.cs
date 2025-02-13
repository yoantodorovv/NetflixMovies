using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.AppServices.ShowAppService.Interface;
using Movies.Dtos.Show;

namespace Movies.App.Controllers;

public class ShowController : Controller
{
    private readonly int _itemsPerPage = 6;
    
    private readonly IShowAppService _showService;
    
    public ShowController(IShowAppService showService)
    {
        _showService = showService;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> All(int id = 1)
    {
        var model = new AllShowsDto()
        {
            ControllerName = "Show",
            ActionName = "All",
            ItemsPerPage = _itemsPerPage,
            PageNumber = id,
            EntityCount = await _showService.GetCountAsync(),
            Shows = await _showService.GetPagedAsync(id, _itemsPerPage)
        };
        
        return View(model);
    }
}