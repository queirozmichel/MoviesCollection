using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;

namespace MoviesCollection.Api.Repository
{
  public class ParentalRatingRepository : Repository<ParentalRating>, IParentalRatingRepository
  {
    public ParentalRatingRepository(ApiDbContext context) : base(context)
    {
    }

    public async Task<PagedList<ParentalRating>> GetParentalRatings(ParentalRatingsParameters parentalRatingsParameters)
    {
      return await PagedList<ParentalRating>.ToPagedList(Get().OrderBy(x => x.Description), parentalRatingsParameters.PageNumber, parentalRatingsParameters.PageSize);
    }
  }
}
