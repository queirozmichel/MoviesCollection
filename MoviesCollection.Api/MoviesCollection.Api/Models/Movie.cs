using System.ComponentModel.DataAnnotations;

namespace MoviesCollection.Api.Models
{
  public class Movie
  {
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Title { get; set; }

    [Required]
    [StringLength(100)]
    public string? OriginalTitle { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; }

    [Required]
    public int Runtime { get; set; }

    public int DirectorId { get; set; }

    public Director? Director { get; set; }

    public int LanguageId { get; set; }

    public Language? Language { get; set; }

    public int CountryId { get; set; }

    public Country? Country { get; set; }

    [Required]
    public DateOnly ReleaseDate { get; set; }

    public int ParentalRatingId { get; set; }

    public ParentalRating? ParentalRating { get; set; }

    [Required]
    public int Evaluation { get; set; }

    [Required]
    [StringLength(2000)]
    public string? Synopsis { get; set; }

    [StringLength(500)]
    public string? Teaser { get; set; }

    public byte[]? Poster { get; set; }
  }
}
