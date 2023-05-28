﻿using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public interface ILanguageRepository : IRepository<Language>
  {
    PagedList<Language> GetLanguages(LanguageParameters languageParameters);
  }
}
