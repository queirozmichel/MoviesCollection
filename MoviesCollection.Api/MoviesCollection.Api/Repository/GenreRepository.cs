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

    public async Task<PagedList<Genre>> GetGenres(GenresParameters genresParameters)
    {
      return await PagedList<Genre>.ToPagedList(Get().OrderBy(x => x.Description), genresParameters.PageNumber, genresParameters.PageSize);
    }
  }
}
