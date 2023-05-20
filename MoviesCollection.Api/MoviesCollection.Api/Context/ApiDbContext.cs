using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Context
{
  public class ApiDbContext :DbContext
  {
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<ParentalRating> ParentalRatings { get; set; }
  }
}
