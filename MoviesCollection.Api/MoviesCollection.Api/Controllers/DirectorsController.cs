using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class DirectorsController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public DirectorsController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Director>> Get()
    {
      List<Director>? directors = new();

      try
      {
        directors = _context.Directors.AsNoTracking().ToList();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (directors is null)
      {
        return NotFound("Diretores não encontrados.");
      }
      return directors;
    }

    [HttpGet("{id:int}", Name = "GetDirector")]
    public ActionResult<Director> Get(int id)
    {
      Director? director = new();
      try
      {
        director = _context.Directors.AsNoTracking().FirstOrDefault(x => x.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (director == null)
      {
        return NotFound($"Diretor com id {id} não encontrado.");
      }
      return director;
    }

    [HttpPost]
    public ActionResult Post(Director director)
    {
      if (director is null)
      {
        return BadRequest();
      }

      try
      {
        _context.Add(director);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetDirector", new { id = director.Id }, director);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Director director)
    {
      if (id != director.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.Entry(director).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(director);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      Director? director = new();

      try
      {
        director = _context.Directors.AsNoTracking().FirstOrDefault(director => director.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (director is null)
      {
        return NotFound($"Diretor com id {id} não encontrado.");
      }

      try
      {
        _context.Directors.Remove(director);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return Ok(director);
    }
  }
}
