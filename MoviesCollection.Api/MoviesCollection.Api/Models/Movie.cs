using System.ComponentModel.DataAnnotations;

namespace MoviesCollection.Api.Models
{
  public class Movie
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Title é obrigatório.")]
    [StringLength(100, ErrorMessage = "Title deve ter no máximo {1} caracteres.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "OriginalTitle é obrigatório.")]
    [StringLength(100, ErrorMessage = "OriginalTitle deve ter no máximo {1} caracteres.")]
    public string? OriginalTitle { get; set; }

    [Range(1, 1000, ErrorMessage ="GenreId deve estar entre {1} e {2}.")]
    public int GenreId { get; set; }

    public Genre? Genre { get; set; }

    [Required(ErrorMessage = "Runtime é obrigatório.")]
    [Range(1, 1000, ErrorMessage = "Runtime deve estar entre {1} e {2}.")]
    public int Runtime { get; set; }

    [Range(1, 1000, ErrorMessage = "DirectorId deve estar entre {1} e {2}.")]
    public int DirectorId { get; set; }

    public Director? Director { get; set; }

    [Range(1, 1000, ErrorMessage = "LanguageId deve estar entre {1} e {2}.")]
    public int LanguageId { get; set; }

    public Language? Language { get; set; }

    [Range(1, 1000, ErrorMessage = "CountryId deve estar entre {1} e {2}.")]
    public int CountryId { get; set; }

    public Country? Country { get; set; }

    [Required(ErrorMessage = "ReleaseDate é obrigatório.")]
    public DateOnly ReleaseDate { get; set; }

    [Range(1, 1000, ErrorMessage = "ParentalRatingId deve estar entre {1} e {2}.")]
    public int ParentalRatingId { get; set; }

    public ParentalRating? ParentalRating { get; set; }

    [Required(ErrorMessage = "Evaluation é obrigatório.")]
    [Range(0, 5, ErrorMessage = "Evaluation deve estar entre {1} e {2}.")]
    public int Evaluation { get; set; }

    [Required(ErrorMessage = "Synopsis é obrigatório.")]
    [StringLength(2000, ErrorMessage = "Synopsis deve ter no máximo {1} caracteres.")]
    public string? Synopsis { get; set; }

    [StringLength(500, ErrorMessage = "Teaser deve ter no máximo {1} caracteres.")]
    public string? Teaser { get; set; }

    public byte[]? Poster { get; set; }
  }
}
