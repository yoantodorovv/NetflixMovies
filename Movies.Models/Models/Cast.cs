using System.ComponentModel.DataAnnotations;
using Movies.Models.Models.Base;

namespace Movies.Models.Models;

public class Cast : BaseEntity<int>
{
    public Cast()
    {
        this.Shows = new HashSet<Show>();
    }
    
    [Required]
    [StringLength(300, MinimumLength = 1)]
    public string Name { get; set; }

    public ICollection<Show> Shows { get; set; }
}