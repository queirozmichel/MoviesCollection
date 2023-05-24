using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public class MovieRepository : Repository<Movie>, IMovieRepository
  {
    public MovieRepository(ApiDbContext context) : base(context)
    {
    }
  }
}
