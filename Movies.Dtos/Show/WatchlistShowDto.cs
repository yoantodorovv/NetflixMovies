namespace Movies.Dtos.Show;

public class WatchlistShowDto
{
    public WatchlistShowDto(Guid id, string title, string type)
    {
        Id = id;
        Title = title;
        Type = type;
    }

    public Guid Id { get; set; }
    
    public string Title { get; set; }

    public string Type { get; set; }
}