using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Context
{
  public class ApiDbContext : DbContext
  {
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<ParentalRating> ParentalRatings { get; set; }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
      builder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>().HaveColumnType("date");
      base.ConfigureConventions(builder);
    }
  }

  public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
  {
    public DateOnlyConverter() : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), dateTime => DateOnly.FromDateTime(dateTime)) { }
  }
}
