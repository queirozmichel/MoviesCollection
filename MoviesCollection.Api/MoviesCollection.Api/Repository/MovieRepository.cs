using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public class MovieRepository : Repository<Movie>, IMovieRepository
  {
    public MovieRepository(ApiDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Movie>> Get(MoviesParameters moviesParameters)
    {
      return await PagedList<Movie>.ToPagedList(Get().OrderBy(x => x.Title), moviesParameters.PageNumber, moviesParameters.PageSize);
    }
  }
}
