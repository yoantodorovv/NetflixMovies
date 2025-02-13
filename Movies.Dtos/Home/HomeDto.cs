using Movies.Dtos.Show;
using Movies.Dtos.Show.Interfaces;

namespace Movies.Dtos.Home;

public class HomeDto
{
    public HomeDto(
        List<RecentlyViewedShowDto> recentlyViewedShowDtos,
        ICollection<IShowInListDto> shortestMovies,
        ICollection<IShowInListDto> shortestSeries)
    {
        RecentlyViewedShowDtos = recentlyViewedShowDtos;
        ShortestMovies = shortestMovies;
        ShortestSeries = shortestSeries;
    }

    public ICollection<RecentlyViewedShowDto> RecentlyViewedShowDtos { get; init; }

    public bool HasRecentlyViewed => RecentlyViewedShowDtos.Count > 0;

    public ICollection<IShowInListDto> ShortestMovies { get; set; }

    public ICollection<IShowInListDto> ShortestSeries { get; set; }
}