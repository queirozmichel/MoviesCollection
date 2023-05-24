using MoviesCollection.Api.Context;

namespace MoviesCollection.Api.Repository
{
  public class UnityOfWork : IUnitOfWork
  {
    private GenreRepository? _genreRepository;
    private DirectorRepository? _directorRepository;
    private LanguageRepository? _languageRepository;
    private CountryRepository? _countryRepository;
    private ParentalRatingRepository? _parentalRatingRepository;
    private MovieRepository? _movieRepository;
    public ApiDbContext _context;

    public UnityOfWork(ApiDbContext context)
    {
      _context = context;
    }

    public IGenreRepository GenreRepository
    {
      get
      {
        return _genreRepository = _genreRepository ?? new GenreRepository(_context);
      }
    }

    public IDirectorRepository DirectorRepository
    {
      get
      {
        return _directorRepository = _directorRepository ?? new DirectorRepository(_context);
      }
    }

    public IMovieRepository MovieRepository
    {
      get
      {
        return _movieRepository = _movieRepository ?? new MovieRepository(_context);
      }
    }

    public ILanguageRepository LanguageRepository
    {
      get
      {
        return _languageRepository = _languageRepository ?? new LanguageRepository(_context);
      }
    }

    public ICountryRepository CountryRepository
    {
      get
      {
        return _countryRepository = _countryRepository ?? new CountryRepository(_context);
      }
    }

    public IParentalRatingRepository ParentalRatingRepository
    {
      get
      {
        return _parentalRatingRepository = _parentalRatingRepository ?? new ParentalRatingRepository(_context);
      }
    }

    public void Commit()
    {
      _context.SaveChanges();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
