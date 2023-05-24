using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/movies")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public MoviesController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> Get()
    {
      List<Movie> movies = new();

      try
      {
        movies = await _context.Movies.AsNoTracking().Include(g => g.Genre).Include(d => d.Director).Include(c => c.Country)
        .Include(l => l.Language).Include(p => p.ParentalRating).ToListAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (movies is null)
      {
        return NotFound("Filmes não foram encontrados.");
      }

      return movies;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetMovie")]
    public async Task<ActionResult<Movie>> Get(int id)
    {
      Movie? movie = new();

      try
      {
        movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(movie => movie.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (movie is null)
      {
        return NotFound($"Filme com id {id} não encontrado.");
      }

      return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Movie movie)
    {
      if (movie == null)
      {
        return BadRequest();
      }

      try
      {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetMovie", new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, Movie movie)
    {
      if (id != movie.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.Entry(movie).State = EntityState.Modified;
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(movie);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult> Delete(int id)
    {
      Movie? movie = new();

      try
      {
        movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(_movie => _movie.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (movie is null)
      {
        return NotFound($"Filme com o id {id} não encontrado.");
      }

      try
      {
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(movie);
    }
  }
}
