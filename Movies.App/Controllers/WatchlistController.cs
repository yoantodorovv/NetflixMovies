using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.App.Extensions;
using Movies.Dtos.Category;
using Movies.Dtos.Show;
using Movies.Models.Models;

namespace Movies.App.Controllers;

[Authorize]
public class WatchlistController : Controller
{
    private const string WatchlistSessionKey = "Watchlist";
    
    public WatchlistController()
    {
        
    }
    
    public IActionResult Index()
    {
        var watchlist = HttpContext.Session.GetObject<List<WatchlistShowDto>>(WatchlistSessionKey) ?? new List<WatchlistShowDto>();
        return View(watchlist);
    }
    
    public IActionResult Add(string idRaw, string title, string type)
    {
        var id = Guid.Parse(idRaw);
        var watchlist = HttpContext.Session.GetObject<List<WatchlistShowDto>>(WatchlistSessionKey) ?? new List<WatchlistShowDto>();

        if (watchlist.All(m => m.Id != id))
        {
            watchlist.Add(new WatchlistShowDto(id, title, type));
            HttpContext.Session.SetObject(WatchlistSessionKey, watchlist);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Remove(string idRaw)
    {
        var id = Guid.Parse(idRaw);
        
        var watchlist = HttpContext.Session.GetObject<List<WatchlistShowDto>>(WatchlistSessionKey) ?? new List<WatchlistShowDto>();
        watchlist = watchlist.Where(m => m.Id != id).ToList();
        HttpContext.Session.SetObject(WatchlistSessionKey, watchlist);

        return RedirectToAction("Index");
    }

    public IActionResult Clear()
    {
        HttpContext.Session.Remove(WatchlistSessionKey);
        return RedirectToAction("Index");
    }
}