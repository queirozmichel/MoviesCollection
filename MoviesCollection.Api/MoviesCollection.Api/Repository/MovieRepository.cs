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

    public PagedList<Movie> Get(MoviesParameters moviesParameters)
    {
      return PagedList<Movie>.ToPagedList(Get().OrderBy(x => x.Title), moviesParameters.PageNumber, moviesParameters.PageSize);
    }
  }
}
