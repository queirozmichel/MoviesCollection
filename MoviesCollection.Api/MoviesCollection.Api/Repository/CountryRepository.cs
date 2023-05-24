using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public class CountryRepository : Repository<Country>, ICountryRepository
  {
    public CountryRepository(ApiDbContext context) : base(context)
    {
    }
  }
}
