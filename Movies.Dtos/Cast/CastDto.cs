namespace Movies.Dtos.Cast;

public class CastDto
{
    public CastDto(Models.Models.Cast cast)
    {
        Name = cast.Name;
    }

    public string Name { get; set; }
}