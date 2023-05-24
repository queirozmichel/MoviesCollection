using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public class GenreRepository : Repository<Genre>, IGenreRepository
  {
    public GenreRepository(ApiDbContext context) : base(context)
    {
    }
  }
}
