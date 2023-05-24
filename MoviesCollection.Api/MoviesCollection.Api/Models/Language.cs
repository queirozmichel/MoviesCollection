using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesCollection.Api.Models
{
  public class Language
  {
    public Language()
    {
      Movies = new Collection<Movie>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Description é obrigatório.")]
    [StringLength(50, ErrorMessage = "Description deve ter no máximo {1} caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "LanguageCode é obrigatório.")]
    [StringLength(10, ErrorMessage = "LanguageCode deve ter no máximo {1} caracteres.")]
    public string? LanguageCode { get; set; }

    [JsonIgnore]
    public ICollection<Movie>? Movies { get; set; }
  }
}
