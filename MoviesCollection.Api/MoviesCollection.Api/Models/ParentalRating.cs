using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesCollection.Api.Models
{
  public class ParentalRating
  {
    public ParentalRating()
    {
      Movies = new Collection<Movie>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage ="Description é obrigatório.")]
    [StringLength(100, ErrorMessage ="Description deve ter no máximo {1} caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Violence é obrigatório.")]
    [StringLength(500, ErrorMessage = "Violence deve ter no máximo {1} caracteres.")]
    public string? Violence { get; set; }

    [Required(ErrorMessage = "SexAndNudity é obrigatório.")]
    [StringLength(500, ErrorMessage = "SexAndNudity deve ter no máximo {1} caracteres.")]
    public string? SexAndNudity { get; set; }

    [Required(ErrorMessage = "Drugs é obrigatório.")]
    [StringLength(500, ErrorMessage = "Drugs deve ter no máximo {1} caracteres.")]
    public string? Drugs { get; set; }

    public byte[]? Image { get; set; }

    [JsonIgnore]
    public ICollection<Movie>? Movies { get; set; }
  }
}
