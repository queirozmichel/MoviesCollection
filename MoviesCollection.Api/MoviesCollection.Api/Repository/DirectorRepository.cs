using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public class DirectorRepository : Repository<Director>, IDirectorRepository
  {
    public DirectorRepository(ApiDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Director>> GetDirectors(DirectorsParameters directorsParameters)
    {
      return await PagedList<Director>.ToPagedList(Get().OrderBy(x => x.Name), directorsParameters.PageNumber, directorsParameters.PageSize);
    }
  }
}
