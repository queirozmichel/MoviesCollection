using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public interface ICountryRepository : IRepository<Country>
  {
    Task<PagedList<Country>> GetCountries(CountriesParameters countriesParameters);
  }
}
