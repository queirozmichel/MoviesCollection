using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesCollection.Api.Models
{
  public class ParentalRating
  {
    public ParentalRating()
    {
      Movies = new Collection<Movie>();
    }

    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Description { get; set; }

    [Required]
    [StringLength(500)]
    public string? Violence { get; set; }

    [Required]
    [StringLength(500)]
    public string? SexAndNudity { get; set; }

    [Required]
    [StringLength(500)]
    public string? Drugs { get; set; }

    public byte[]? Image { get; set; }

    public ICollection<Movie>? Movies { get; set; }
  }
}
