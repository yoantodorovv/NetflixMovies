using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Models.Models;
using Movies.Models.Models.Identity;

namespace Movies.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext()
    {
        
    }
    
    // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //     : base(options)
    // {}

    public DbSet<Show> Shows { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Cast> Cast { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    // TODO: Get con string form config
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=MoviesTest;User Id=sa;Password=Password123$;");
        
        base.OnConfiguring(optionsBuilder);
    }
}