namespace Movies.Dtos.Rating;

public class RatingDto
{
    public RatingDto(Models.Models.Rating? rating)
    {
        Type = rating?.Type ?? "Not Specified";
    }
    
    public string Type { get; set; }
}