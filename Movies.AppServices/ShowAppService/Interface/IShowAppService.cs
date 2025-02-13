using Movies.Common.Enumerations;
using Movies.Dtos.Show;
using Movies.Dtos.Show.Interfaces;

namespace Movies.AppServices.ShowAppService.Interface;

public interface IShowAppService
{
    public Task<ShowDto> GetById(Guid id);
    public Task<ICollection<ShowPagedDto>> GetPagedAsync(int page, int itemsPerPage);
    public Task<int> GetCountAsync();
    public Task<ICollection<IShowInListDto>> GetShortestByType(DurationType durationType);
}