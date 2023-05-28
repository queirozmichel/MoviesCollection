using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public class CountryRepository : Repository<Country>, ICountryRepository
  {
    public CountryRepository(ApiDbContext context) : base(context)
    {
    }

    public PagedList<Country> GetCountries(CountriesParameters countriesParameters)
    {
      return PagedList<Country>.ToPagedList(Get().OrderBy(x => x.Name), countriesParameters.PageNumber, countriesParameters.PageSize);
    }
  }
}
