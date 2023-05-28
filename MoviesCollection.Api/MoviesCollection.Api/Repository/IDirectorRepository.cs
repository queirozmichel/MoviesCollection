using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public interface IDirectorRepository : IRepository<Director>
  {
    Task<PagedList<Director>> GetDirectors(DirectorsParameters directorsParameters);
  }
}
