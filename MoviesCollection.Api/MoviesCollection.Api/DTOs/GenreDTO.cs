namespace MoviesCollection.Api.DTOs
{
  public class GenreDTO
  {
    public int Id { get; set; }
    public string? Description { get; set; }
    public byte[]? Image { get; set; }
  }
}