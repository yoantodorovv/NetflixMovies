using Movies.Dtos.Show;

namespace Movies.AppServices.ShowAppService.Interface;

public interface IShowAppService
{
    public Task<ShowDto> GetById(Guid id);
    public Task<ICollection<ShowPagedDto>> GetPagedAsync(int page, int itemsPerPage);
    public Task<int> GetCountAsync();
}