using Movies.Dtos.Shared;

namespace Movies.Dtos.Show;

public class AllShowsDto : PaginationDto
{
    public ICollection<ShowPagedDto> Shows { get; set; }
}