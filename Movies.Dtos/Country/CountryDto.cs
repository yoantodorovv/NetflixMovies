namespace Movies.Dtos.Country;

public class CountryDto
{
    public CountryDto(Models.Models.Country country)
    {
        Name = country.Name;
    }
    
    public string Name { get; set; }
}