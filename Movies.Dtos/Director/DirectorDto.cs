namespace Movies.Dtos.Director;

public class DirectorDto
{
    public DirectorDto(Models.Models.Director director)
    {
        Name = director?.Name ?? "";
    }
    
    public string Name { get; set; }
}