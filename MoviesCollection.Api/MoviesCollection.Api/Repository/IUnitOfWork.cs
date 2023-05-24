namespace MoviesCollection.Api.Repository
{
  public interface IUnitOfWork
  {
    IGenreRepository GenreRepository { get; }
    IDirectorRepository DirectorRepository { get; }
    IMovieRepository MovieRepository { get; }
    ILanguageRepository LanguageRepository { get; }
    ICountryRepository CountryRepository { get; }
    IParentalRatingRepository ParentalRatingRepository { get; }

    void Commit();
  }
}
