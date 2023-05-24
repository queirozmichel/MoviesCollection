using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public class LanguageRepository : Repository<Language>, ILanguageRepository
  {
    public LanguageRepository(ApiDbContext context) : base(context)
    {
    }
  }
}
