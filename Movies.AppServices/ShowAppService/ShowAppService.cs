using Microsoft.EntityFrameworkCore;
using Movies.AppServices.ShowAppService.Interface;
using Movies.Data;
using Movies.Dtos.Show;
using Movies.Models.Models;

namespace Movies.AppServices.ShowAppService;

public class ShowAppService : IShowAppService
{
    private readonly ApplicationDbContext _context;
    
    public ShowAppService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Show> ShowsQueryableWithoutIncludings => _context
        .Shows
        .AsNoTracking();
    
    public IQueryable<Show> ShowsQueryableWithIncludings => _context
        .Shows
        .AsNoTracking()
        .Include(x => x.Directors)
        .Include(x => x.Cast)
        .Include(x => x.Categories)
        .Include(x => x.Countries)
        .Include(x => x.Rating);

    public async Task<ICollection<ShowPagedDto>> GetPagedAsync(int page, int itemsPerPage) => await ShowsQueryableWithIncludings
        .Skip((page - 1) * itemsPerPage)
        .Take(itemsPerPage)
        .Select(show => new ShowPagedDto(show))
        .ToListAsync();

    public async Task<int> GetCountAsync() => await ShowsQueryableWithoutIncludings.CountAsync();
}