using Movies.Common.Enumerations;
using Movies.Dtos.Category;
using Movies.Dtos.Director;
using Movies.Models.Models;

namespace Movies.Dtos.Show;

public class ShowPagedDto
{
    public ShowPagedDto(Models.Models.Show show)
    {
        Id = show.Id;
        Type = show.Type;
        Title = show.Title;
        DateAdded = show.DateAdded;
        DurationType = show.DurationType;
        DurationValue = show.DurationValue;
        Directors = show.Directors.Select(x => new DirectorDto(x)).ToList();
        Categories = show.Categories.Select(x => new CategoryDto(x)).ToList();
        Rating = show.Rating?.Type ?? "Not Rated";
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
    
    public DurationType DurationType { get; set; }

    public int DurationValue { get; set; }

    public string DurationText => DurationType switch
    {
        DurationType.Minutes => $"{DurationValue} minutes",
        DurationType.Seasons => $"{DurationValue} Seasons",
        _ => $"{DurationValue} minutes"
    };
    
    public ICollection<DirectorDto> Directors { get; set; }

    public string DirectorsText => string.Join(", ", Directors.Select(x => x.Name));

    public ICollection<CategoryDto> Categories { get; set; }

    public ICollection<CategoryDto> CategoriesToVisualise => Categories.Take(1).ToList();

    public bool HasMoreCategoriesToVisualise => Categories.Count > 1;

    public string MoreCategoriesText => HasMoreCategoriesToVisualise ? $"+ { Categories.Count - 1 } more" : "";

    public string CategoriesText => $"{string.Join(", ", CategoriesToVisualise.Select(x => x.Name))} {MoreCategoriesText}";
    public string Rating { get; set; }
}