namespace MoviesCollection.Api.Pagination
{
  public class PagedList<T> : List<T>
  {
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    //Se currentPage for > 1, HasPrevious = true;
    public bool HasPrevious => CurrentPage > 1;
    //Se currentPage for < TotalPages, HasPrevious = true;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
      TotalCount = count;
      PageSize = pageSize;
      CurrentPage = pageNumber;
      TotalPages = (int)Math.Ceiling(count / (double)pageSize);
      AddRange(items);
    }

    public PagedList()
    {
    }

    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
      var count = source.Count();
      var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
      return new PagedList<T>(items, count, pageNumber, pageSize);
    }
  }
}
