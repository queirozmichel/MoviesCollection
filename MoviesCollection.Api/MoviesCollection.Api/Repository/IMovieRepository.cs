using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public interface IMovieRepository : IRepository<Movie>
  {
    new IEnumerable<Movie> Get();
  }
}
