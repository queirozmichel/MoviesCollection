using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesCollection.Api.Models
{
  public class Genre
  {
    public Genre()
    {
      Movies = new Collection<Movie>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Description é obrigatório.")]
    [StringLength(50, ErrorMessage = "Description deve ter no máximo {1} caracteres.")]
    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    [JsonIgnore]
    public ICollection<Movie>? Movies { get; set; }
  }
}
