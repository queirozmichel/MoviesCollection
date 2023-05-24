using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public class DirectorRepository : Repository<Director>, IDirectorRepository
  {
    public DirectorRepository(ApiDbContext context) : base(context)
    {
    }
  }
}
