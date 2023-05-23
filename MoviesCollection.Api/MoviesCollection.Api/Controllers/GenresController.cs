using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class GenresController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public GenresController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Genre>> Get()
    {
      List<Genre> genres = new();

      try
      {
        genres = _context.Genres.AsNoTracking().ToList();
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

    [HttpGet("{id:int}", Name = "GetGenre")]
    public ActionResult<Genre> Get(int id)
    {
      Genre? genre = new();
      try
      {
        genre = _context.Genres.AsNoTracking().FirstOrDefault(genre => genre.Id == id);
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
    public ActionResult Post(Genre genre)
    {
      if (genre is null)
      {
        return BadRequest();
      }

      try
      {
        _context.Genres.Add(genre);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetGenre", new { id = genre.Id }, genre);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Genre genre)
    {
      if (id != genre.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.Entry(genre).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(genre);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      Genre? genre = new();

      try
      {
        genre = _context.Genres.AsNoTracking().FirstOrDefault(genre => genre.Id == id);
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
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(genre);
    }
  }
}
