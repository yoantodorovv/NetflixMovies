using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Movies.Common.Enumerations;
using Movies.Models.Models.Base;

namespace Movies.Models.Models;

public class Show : BaseEntity<Guid>
{
    public Show()
    {
        this.Directors = new HashSet<Director>();
        this.Cast = new HashSet<Cast>();
        this.Countries = new HashSet<Country>();
        this.Categories = new HashSet<Category>();
    }
    
    public ShowType Type { get; set; }

    [Required]
    [Range(1, 300)]
    public string Title { get; set; }

    public DateTime? DateAdded { get; set; }
    
    public int ReleaseYear { get; set; }

    public DurationType DurationType { get; set; }

    public int DurationValue { get; set; }
    
    [Required]
    [Range(1, 1000)]
    public string Description { get; set; }
    
    public ICollection<Director> Directors { get; set; }
    
    public ICollection<Cast> Cast { get; set; }
    
    public ICollection<Country> Countries { get; set; }

    public ICollection<Category> Categories { get; set; }

    [ForeignKey(nameof(Rating))]
    public int RatingId { get; set; }
    public Rating? Rating { get; set; }
}