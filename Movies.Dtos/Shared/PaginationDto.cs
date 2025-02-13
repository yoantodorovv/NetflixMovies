namespace Movies.Dtos.Shared;

public class PaginationDto
{
    public string ActionName { get; set; }

    public string ControllerName { get; set; }

    public int PageNumber { get; set; }

    public int EntityCount { get; set; }

    public int ItemsPerPage { get; set; }

    public bool HasPreviousPage => this.PageNumber > 1;

    public int PreviousPageNumber => this.PageNumber - 1;

    public bool HasNextPage => this.PageNumber < this.PagesCount;

    public int NextPageNumber => this.PageNumber + 1;

    public int PagesCount => (int)Math.Ceiling((double)this.EntityCount / this.ItemsPerPage);
}