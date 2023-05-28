using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public interface IMovieRepository : IRepository<Movie>
  {
    PagedList<Movie> Get(MoviesParameters moviesParameters);
  }
}
