using Movies.Common.Enumerations;
using Movies.Dtos.Cast;
using Movies.Dtos.Category;
using Movies.Dtos.Country;
using Movies.Dtos.Director;
using Movies.Dtos.Rating;
using Movies.Models.Models;

namespace Movies.Dtos.Show;

public class ShowSingleDto
{
    public ShowSingleDto(ShowDto show)
    {
        Id = show.Id;
        Type = show.Type;
        Title = show.Title;
        DateAdded = show.DateAdded;
        ReleaseYear = show.ReleaseYear;
        Description = show.Description;
        Directors = show.Directors.Select(x => new DirectorDto(x)).ToList();
        Cast = show.Cast.Select(x => new CastDto(x)).ToList();
        Countries = show.Countries.Select(x => new CountryDto(x)).ToList();
        Categories = show.Categories.Select(x => new CategoryDto(x)).ToList();
        Rating = new RatingDto(show.Rating);
        DurationType = show.DurationType;
        DurationValue = show.DurationValue;
    }
    
    public Guid Id { get; set; }
    
    public ShowType Type { get; set; }
    
    public string ShowTypeText => Type switch
    {
        ShowType.Movie => "Movie",
        ShowType.TvShow => "Series",
        ShowType.None => "Not Specified",
        _ => "Not Specified"
    };


    public string Title { get; set; }

    public DateTime? DateAdded { get; set; }
    
    public string DateAddedText => DateAdded.HasValue ? DateAdded.Value.Date.ToString("dd/MM/yyyy") : "Not Specified";
    
    public int ReleaseYear { get; set; }
    
    public string Description { get; set; }

    public DurationType DurationType { get; set; }

    public int DurationValue { get; set; }
    
    public string DurationText => DurationType switch
    {
        DurationType.Minutes => $"{DurationValue} minutes",
        DurationType.Seasons => $"{DurationValue} Seasons",
        _ => $"{DurationValue} minutes"
    };
    
    public ICollection<DirectorDto> Directors { get; set; }
    
    public string DirectorsText => Directors.Count == 0 
        ? "Not Listed"
        : string.Join(", ", Directors.Select(x => x.Name));
    
    public ICollection<CastDto> Cast { get; set; }
    
    public string CastText => Cast.Count == 0 
        ? "Not Listed"
        : $"{string.Join(", ", Cast.Select(x => x.Name))}";
    
    public ICollection<CountryDto> Countries { get; set; }
    
    public string CountriesText => Countries.Count == 0 
        ? "Not Listed"
        : $"{string.Join(", ", Countries.Select(x => x.Name))}";

    public ICollection<CategoryDto> Categories { get; set; }
    
    public string CategoriesText => Categories.Count == 0 
        ? "Not Listed"
        : $"{string.Join(", ", Categories.Select(x => x.Name))}";

    public RatingDto? Rating { get; set; }

    public string RatingText => Rating?.Type ?? "Not Rated";
}