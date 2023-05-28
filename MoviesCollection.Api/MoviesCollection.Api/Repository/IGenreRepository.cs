using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public interface IGenreRepository : IRepository<Genre>
  {
    Task<PagedList<Genre>> GetGenres(GenresParameters genresParameters);
  }
}
