using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public interface IParentalRatingRepository : IRepository<ParentalRating>
  {
    PagedList<ParentalRating> GetParentalRatings(ParentalRatingsParameters parentalRatingsParameters);
  }
}
