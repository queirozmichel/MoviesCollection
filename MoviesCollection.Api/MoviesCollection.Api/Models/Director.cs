using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesCollection.Api.Models
{
  public class Director
  {
    public Director()
    {
      Movies = new Collection<Movie>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Name é obrigatório.")]
    [StringLength(50, ErrorMessage = "Name deve ter no máximo {1} caracteres.")]
    public string? Name { get; set; }

    public byte[]? Image { get; set; }

    [JsonIgnore]
    public ICollection<Movie>? Movies { get; set; }
  }
}
