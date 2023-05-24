using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/genres")]
  [ApiController]
  public class GenresController : ControllerBase
  {
    private readonly IUnitOfWork _context;

    public GenresController(IUnitOfWork context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Genre>> Get()
    {
      List<Genre> genres = new();

      try
      {
        genres = _context.GenreRepository.Get().ToList();
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
    public ActionResult<Genre> Get(int id)
    {
      Genre? genre = new();
      try
      {
        genre = _context.GenreRepository.GetById(genre => genre.Id == id);
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
        _context.GenreRepository.Add(genre);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetGenre", new { id = genre.Id }, genre);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Genre genre)
    {
      if (id != genre.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.GenreRepository.Update(genre);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(genre);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
      Genre? genre = new();

      try
      {
        genre = _context.GenreRepository.GetById(genre => genre.Id == id);
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
        _context.GenreRepository.Delete(genre);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(genre);
    }
  }
}
