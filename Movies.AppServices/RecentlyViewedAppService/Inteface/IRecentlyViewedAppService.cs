using Movies.Dtos.Show;

namespace Movies.AppServices.RecentlyViewedAppService.Inteface;

public interface IRecentlyViewedAppService
{
    public void SaveRecentlyViewed(RecentlyViewedShowDto show);
    public List<RecentlyViewedShowDto> GetRecentlyViewed();
}