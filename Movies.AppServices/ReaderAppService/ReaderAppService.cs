using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Movies.AppServices.ReaderAppService.Interface;
using Movies.Common.Enumerations;
using Movies.Data;
using Movies.Dtos.Duration;
using Movies.Dtos.Reader;
using Movies.Models.Models;

namespace Movies.AppServices.ReaderAppService;

public class ReaderAppService : IReaderAppService
{
    private readonly ApplicationDbContext _context;

    private readonly Dictionary<string, Rating> _addedRatings;
    private readonly Dictionary<string, Category> _addedCategories;
    private readonly Dictionary<string, Country> _addedCountries;
    private readonly Dictionary<string, Director> _addedDirectors;
    private readonly Dictionary<string, Cast> _addedCast;
    private readonly Dictionary<string, Show> _addedShows;
    
    public ReaderAppService(ApplicationDbContext context)
    {
        _context = context;
        
        _addedCountries = new Dictionary<string, Country>();
        _addedDirectors = new Dictionary<string, Director>();
        _addedCast = new Dictionary<string, Cast>();
        _addedShows = new Dictionary<string, Show>();
        _addedRatings = new Dictionary<string, Rating>();
        _addedCategories = new Dictionary<string, Category>();
    }

    public async Task SeedDatabaseAsync()
    {
        if (await _context.Shows.AnyAsync())
        {
            return;
        }
        
        await SeedDatabaseInternalAsync();
    }

    private async Task SeedDatabaseInternalAsync()
    {
        var records = await ReadRecordsAsync();
        
        foreach (var record in records)
        {
            var rating = AddRating(record.Rating);
            var categories = AddCategories(record.Categories);
            var countries = AddCountries(record.Country);
            var directors = AddDirectors(record.Directors);
            var cast = AddCast(record.Cast);
            var duration = AddDuration(record.Duration);

            var showType = Enum.TryParse<ShowType>(record.Type, out var type) ? type : ShowType.None;
            var dateAdded = DateTime.TryParse(record.DateAdded, out var date) ? date : (DateTime?)null;
            
            var show = new Show()
            {
                Type = showType,
                Title = record.Title,
                ReleaseYear = int.Parse(record.ReleaseYear),
                Description = record.Description,
                DurationType = duration.DurationType,
                DurationValue = duration.Value,
                DateAdded = dateAdded,
                Rating = rating,
                Categories = categories,
                Countries = countries,
                Directors = directors,
                Cast = cast,
            };
            
            _addedShows.Add(show.Title, show);
        }
        
        await _context.Shows.AddRangeAsync(_addedShows.Values);
        await _context.SaveChangesAsync();
    }
    
    private List<Cast> AddCast(string? fullCastRaw)
    {
        if (fullCastRaw is null)
        {
            return new List<Cast>();
        }
        
        var cast = new List<Cast>();
        
        var castList = fullCastRaw.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
        
        foreach (var castRaw in castList)
        {
            if (_addedCast.TryGetValue(castRaw, out var addedCast))
            {
                cast.Add(addedCast);
                continue;
            }
            
            var castMember = new Cast()
            {
                Name = castRaw,
            };
            
            _addedCast.Add(castRaw, castMember);
            cast.Add(castMember);
        }

        return cast;
    }
    
    private List<Director> AddDirectors(string? directorsRaw)
    {
        if (directorsRaw is null)
        {
            return new List<Director>();
        }
        
        var directors = new List<Director>();
        
        var directorsList = directorsRaw.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
        
        foreach (var directorRaw in directorsList)
        {
            if (_addedDirectors.TryGetValue(directorRaw, out var addedDirector))
            {
                directors.Add(addedDirector);
                continue;
            }
            
            var director = new Director()
            {
                Name = directorRaw,
            };
            
            _addedDirectors.Add(directorRaw, director);
            directors.Add(director);
        }

        return directors;
    }

    private List<Country> AddCountries(string? countriesRaw)
    {
        if (countriesRaw is null)
        {
            return new List<Country>();
        }
        
        var countries = new List<Country>();
        
        var countriesList = countriesRaw.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (var countryRaw in countriesList)
        {
            if (_addedCountries.TryGetValue(countryRaw, out var addedCountry))
            {
                countries.Add(addedCountry);
                continue;
            }
            
            var country = new Country()
            {
                Name = countryRaw,
            };
            
            _addedCountries.Add(countryRaw, country);
            countries.Add(country);
        }
        
        return countries;
    }

    private List<Category> AddCategories(string categoriesRaw)
    {
        var categories = new List<Category>();
        
        var categoriesList = categoriesRaw.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (var categoryRaw in categoriesList)
        {
            if (_addedCategories.TryGetValue(categoryRaw, out var addedCategory))
            {
                categories.Add(addedCategory);
                continue;
            }
            
            var category = new Category()
            {
                Name = categoryRaw,
            };
            
            _addedCategories.Add(categoryRaw, category);
            categories.Add(category);
        }
        
        return categories;
    }
    
    private Rating? AddRating(string? ratingRaw)
    {
        if (ratingRaw is null)
        {
            return null;
        }
        
        if (_addedRatings.TryGetValue(ratingRaw, out var addRating))
        {
            return addRating;
        }
        
        var rating = new Rating()
        {
            Type = ratingRaw,
        };
        
        _addedRatings.Add(ratingRaw, rating);
        
        return rating;
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
    
    private static DurationDto AddDuration(string? durationRaw)
    {
        if (durationRaw is null)
        {
            return new DurationDto();
        }
        
        var durationType = GetDurationType(durationRaw);
        var value = GetDurationValue(durationRaw);

        return value is null 
            ? new DurationDto() 
            : new DurationDto(durationType, value.Value);
    }
    
    private static DurationType GetDurationType(string durationRaw) => durationRaw.Contains("min") ? DurationType.Minutes : DurationType.Seasons;
    
    private static int? GetDurationValue(string durationRaw) => int.TryParse(durationRaw.Split(" ")[0], out var value) ? value : null;
}