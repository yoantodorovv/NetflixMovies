using Microsoft.AspNetCore.Http;
using Movies.AppServices.RecentlyViewedAppService.Inteface;
using Movies.Common.Extensions;
using Movies.Dtos.Show;

namespace Movies.AppServices.RecentlyViewedAppService;

public class RecentlyViewedAppService : IRecentlyViewedAppService
{
    private const string RecentlyViewedKey = "RecentlyViewed";
    private const int MaxRecentlyViewed = 5;
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public RecentlyViewedAppService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SaveRecentlyViewed(RecentlyViewedShowDto show)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var recentlyViewed = session.GetObject<List<RecentlyViewedShowDto>>(RecentlyViewedKey) ?? new List<RecentlyViewedShowDto>();

        if (recentlyViewed.Any(m => m.Id == show.Id))
            return;
        
        recentlyViewed.Insert(0, show);
        if (recentlyViewed.Count > MaxRecentlyViewed)
            recentlyViewed = recentlyViewed.Take(MaxRecentlyViewed).ToList();

        session.SetObject(RecentlyViewedKey, recentlyViewed);
    }

    public List<RecentlyViewedShowDto> GetRecentlyViewed()
    {
        return _httpContextAccessor.HttpContext.Session.GetObject<List<RecentlyViewedShowDto>>(RecentlyViewedKey) ?? new List<RecentlyViewedShowDto>();
    }
}