using CsvHelper.Configuration.Attributes;

namespace Movies.Dtos.Reader;

public class ReaderDto
{
    [Name("type")]
    public string Type { get; set; }

    [Name("title")]
    public string Title { get; set; }

    [Name("director")]
    public string? Directors { get; set; }

    [Name("cast")]
    public string? Cast { get; set; }

    [Name("country")]
    public string? Country { get; set; }
    
    [Name("date_added")]
    public string? DateAdded { get; set; }

    [Name("release_year")]
    public string ReleaseYear { get; set; }

    [Name("rating")]
    public string? Rating { get; set; }

    [Name("duration")]
    public string? Duration { get; set; }

    [Name("listed_in")]
    public string Categories { get; set; }

    [Name("description")]
    public string Description { get; set; }
}