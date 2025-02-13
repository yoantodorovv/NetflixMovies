namespace Movies.Dtos.Category;

public class CategoryDto
{
    public CategoryDto(Models.Models.Category category)
    {
        Name = category?.Name ?? "";
    }
    
    public string Name { get; set; }
}