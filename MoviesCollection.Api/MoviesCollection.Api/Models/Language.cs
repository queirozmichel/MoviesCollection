using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesCollection.Api.Models
{
  public class Language
  {
    public Language()
    {
      Movies = new Collection<Movie>();
    }

    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Description { get; set; }

    [Required]
    [StringLength(10)]
    public string? LanguageCode { get; set; }

    public ICollection<Movie>? Movies { get; set; }
  }
}
