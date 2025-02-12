using Movies.Common.Enumerations;
using Movies.Models.Models.Base;

namespace Movies.Models.Models;

public class Duration : BaseEntity<int>
{
    public Duration()
    {
        this.Shows = new HashSet<Show>();
    }
    
    public DurationType Type { get; set; }

    public int Value { get; set; }

    public ICollection<Show> Shows { get; set; }
}