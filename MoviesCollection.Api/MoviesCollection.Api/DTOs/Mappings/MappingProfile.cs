using AutoMapper;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.DTOs.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Genre, GenreDTO>().ReverseMap();
      CreateMap<Director, DirectorDTO>().ReverseMap();
      CreateMap<Language, LanguageDTO>().ReverseMap();
      CreateMap<Country, CountryDTO>().ReverseMap();
      CreateMap<ParentalRating, ParentalRatingDTO>().ReverseMap();
      CreateMap<Movie, MovieDTO>().ReverseMap();
    }
  }
}
