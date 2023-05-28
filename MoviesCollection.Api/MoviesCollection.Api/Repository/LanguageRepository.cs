using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public class LanguageRepository : Repository<Language>, ILanguageRepository
  {
    public LanguageRepository(ApiDbContext context) : base(context)
    {
    }

    public PagedList<Language> GetLanguages(LanguageParameters languageParameters)
    {
      return PagedList<Language>.ToPagedList(Get().OrderBy(x => x.Description), languageParameters.PageNumber, languageParameters.PageSize);
    }
  }
}
