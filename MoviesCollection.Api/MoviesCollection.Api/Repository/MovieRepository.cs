using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public class MovieRepository : Repository<Movie>, IMovieRepository
  {
    public MovieRepository(ApiDbContext context) : base(context)
    {
    }

    IEnumerable<Movie> IMovieRepository.Get()
    {
      return _context.Movies.AsNoTracking().Include(g => g.Genre).Include(d => d.Director).Include(c => c.Country)
        .Include(l => l.Language).Include(p => p.ParentalRating);
    }
  }
}
