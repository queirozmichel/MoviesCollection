using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Repository
{
  public class ParentalRatingRepository : Repository<ParentalRating>, IParentalRatingRepository
  {
    public ParentalRatingRepository(ApiDbContext context) : base(context)
    {
    }
  }
}
