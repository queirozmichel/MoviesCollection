using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public class GenreRepository : Repository<Genre>, IGenreRepository
  {
    public GenreRepository(ApiDbContext context) : base(context)
    {
    }

    public PagedList<Genre> GetGenres(GenresParameters genresParameters)
    {
      return PagedList<Genre>.ToPagedList(Get().OrderBy(x => x.Description), genresParameters.PageNumber, genresParameters.PageSize);
    }
  }
}
