using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/genres")]
  [ApiController]
  public class GenresController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public GenresController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> Get()
    {
      List<Genre> genres = new();

      try
      {
        genres = await _context.Genres.AsNoTracking().ToListAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (genres is null)
      {
        return NotFound("Gêneros não encontrados.");
      }

      return genres;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetGenre")]
    public async Task<ActionResult<Genre>> Get(int id)
    {
      Genre? genre = new();
      try
      {
        genre = await _context.Genres.AsNoTracking().FirstOrDefaultAsync(genre => genre.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (genre is null)
      {
        return NotFound($"Gênero com o id {id} não encontrado.");
      }

      return genre;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Genre genre)
    {
      if (genre is null)
      {
        return BadRequest();
      }

      try
      {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetGenre", new { id = genre.Id }, genre);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, Genre genre)
    {
      if (id != genre.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.Entry(genre).State = EntityState.Modified;
       await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(genre);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult> Delete(int id)
    {
      Genre? genre = new();

      try
      {
        genre = await _context.Genres.AsNoTracking().FirstOrDefaultAsync(genre => genre.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (genre is null)
      {
        return NotFound($"Gênero com id {id} não encontrado");
      }

      try
      {
        _context.Genres.Remove(genre);
       await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(genre);
    }
  }
}
