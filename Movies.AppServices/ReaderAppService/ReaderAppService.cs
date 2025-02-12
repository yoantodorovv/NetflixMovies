using System.Globalization;
using CsvHelper;
using Movies.AppServices.ReaderAppService.Interface;
using Movies.Data;
using Movies.Dtos.Reader;

namespace Movies.AppServices.ReaderAppService;

public class ReaderAppService : IReaderAppService
{
    private readonly ApplicationDbContext _context;
    
    public ReaderAppService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedDatabaseAsync()
    {
        var records = await ReadRecordsAsync();
        
    }

    private async Task<List<ReaderDto>> ReadRecordsAsync()
    {
        var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "data.csv");
        
        using var reader = new StreamReader(dataPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = new List<ReaderDto>();
        
        await foreach (var record in csv.GetRecordsAsync<ReaderDto>())
        {
            records.Add(record);
        }

        return records;
    }
}