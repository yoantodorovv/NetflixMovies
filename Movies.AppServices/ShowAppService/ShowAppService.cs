using Microsoft.EntityFrameworkCore;
using Movies.AppServices.ShowAppService.Interface;
using Movies.Common.Enumerations;
using Movies.Data;
using Movies.Dtos.Show;
using Movies.Dtos.Show.Interfaces;
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
    
    public IQueryable<Show> ShowsQueryableWithAllIncludings => _context
        .Shows
        .AsNoTracking()
        .Include(x => x.Directors)
        .Include(x => x.Cast)
        .Include(x => x.Categories)
        .Include(x => x.Countries)
        .Include(x => x.Rating);

    public IQueryable<Show> ShowsQueryableWithPartialIncludings => _context
        .Shows
        .AsNoTracking()
        .Include(x => x.Directors)
        .Include(x => x.Categories)
        .Include(x => x.Rating);

    public async Task<ShowDto> GetById(Guid id) => await ShowsQueryableWithAllIncludings
        .Where(x => x.Id == id)
        .Select(x => new ShowDto(x))
        .FirstOrDefaultAsync();
    
    public async Task<ICollection<ShowPagedDto>> GetPagedAsync(int page, int itemsPerPage) => await ShowsQueryableWithAllIncludings
        .Skip((page - 1) * itemsPerPage)
        .Take(itemsPerPage)
        .Select(show => new ShowPagedDto(show))
        .ToListAsync();

    public async Task<int> GetCountAsync() => await ShowsQueryableWithoutIncludings.CountAsync();

    public async Task<ICollection<IShowInListDto>> GetShortestByType(DurationType durationType) => await ShowsQueryableWithAllIncludings
        .Where(x => x.DurationType == durationType && x.DurationValue > 0)
        .OrderBy(x => x.DurationValue)
        .Take(5)
        .Select(x => new ShowPagedDto(x))
        .Select(x => x as IShowInListDto)
        .ToListAsync();
}