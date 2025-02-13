using Movies.Common.Enumerations;
using Movies.Models.Models;

namespace Movies.Dtos.Show;

public class ShowDto
{
    public ShowDto()
    {}
    
    public ShowDto(Models.Models.Show show)
    {
        Id = Id;
        Type = show.Type;
        Title = show.Title;
        DateAdded = show.DateAdded;
        ReleaseYear = show.ReleaseYear;
        Description = show.Description;
        Directors = show.Directors;
        Cast = show.Cast;
        Countries = show.Countries;
        Categories = show.Categories;
        RatingId = show.RatingId;
        Rating = show.Rating;
        DurationType = show.DurationType;
        DurationValue = show.DurationValue;
    }

    public Guid Id { get; set; }
    
    public ShowType Type { get; set; }

    public string Title { get; set; }

    public DateTime? DateAdded { get; set; }
    
    public int ReleaseYear { get; set; }
    
    public string Description { get; set; }

    public DurationType DurationType { get; set; }

    public int DurationValue { get; set; }
    
    public ICollection<Models.Models.Director> Directors { get; set; }
    
    public ICollection<Models.Models.Cast> Cast { get; set; }
    
    public ICollection<Models.Models.Country> Countries { get; set; }

    public ICollection<Models.Models.Category> Categories { get; set; }

    public int RatingId { get; set; }
    public Models.Models.Rating? Rating { get; set; }
}