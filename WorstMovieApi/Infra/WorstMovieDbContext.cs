using WorstMovie.Domain;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using WorstMovie.Infra.CsvHelper;

namespace WorstMovie.Infra;
public class WorstMovieDbContext(DbContextOptions<WorstMovieDbContext> options,
    IWebHostEnvironment webHostEnv) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }

    public async void EnsurePopulated()
    {
        try
        {
            var moviesList = LoadCsvMoviesAsync();
            await this.Movies.AddRangeAsync(moviesList);
            await this.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }

    private IEnumerable<Movie> LoadCsvMoviesAsync()
    {
        var path = Path.Combine(webHostEnv.ContentRootPath, "Data", "movielist.csv");

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.UTF8 });
        csv.Context.RegisterClassMap<MovieMap>();
        return csv.GetRecords<Movie>().ToList();
    }
}