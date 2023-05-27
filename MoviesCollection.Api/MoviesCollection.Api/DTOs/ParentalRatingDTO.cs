namespace MoviesCollection.Api.DTOs
{
  public class ParentalRatingDTO
  {
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Violence { get; set; }
    public string? SexAndNudity { get; set; }
    public string? Drugs { get; set; }
    public byte[]? Image { get; set; }
  }
}
