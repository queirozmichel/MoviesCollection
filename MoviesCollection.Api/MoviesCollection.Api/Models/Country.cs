using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesCollection.Api.Models
{
  public class Country
  {
    public Country()
    {
      Movies = new Collection<Movie>();
    }

    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    public byte[]? NationalFlag { get; set; }

    public ICollection<Movie>? Movies { get; set; }
  }
}
