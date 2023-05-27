namespace MoviesCollection.Api.DTOs
{
  public class MovieDTO
  {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? OriginalTitle { get; set; }
    public int GenreId { get; set; }
    public int Runtime { get; set; }
    public int DirectorId { get; set; }
    public int LanguageId { get; set; }
    public int CountryId { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public int ParentalRatingId { get; set; }
    public int Evaluation { get; set; }
    public string? Synopsis { get; set; }
    public string? Teaser { get; set; }
    public byte[]? Poster { get; set; }
  }
}
