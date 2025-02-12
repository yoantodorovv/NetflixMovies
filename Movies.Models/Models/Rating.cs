using System.ComponentModel.DataAnnotations;
using Movies.Models.Models.Base;

namespace Movies.Models.Models;

public class Rating : BaseEntity<int>
{
    public Rating()
    {
        this.Shows = new HashSet<Show>();
    }
    
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Type { get; set; }

    public ICollection<Show> Shows { get; set; }
}