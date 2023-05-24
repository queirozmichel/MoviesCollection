using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/directors")]
  [ApiController]
  public class DirectorsController : ControllerBase
  {
    private readonly IUnitOfWork _context;

    public DirectorsController(IUnitOfWork context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Director>> Get()
    {
      List<Director>? directors = new();

      try
      {
        directors = _context.DirectorRepository.Get().ToList();
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

    [HttpGet("{id:int:min(1)}", Name = "GetDirector")]
    public ActionResult<Director> Get(int id)
    {
      Director? director = new();
      try
      {
        director = _context.DirectorRepository.GetById(x => x.Id == id);
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
        _context.DirectorRepository.Add(director);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetDirector", new { id = director.Id }, director);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Director director)
    {
      if (id != director.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.DirectorRepository.Update(director);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(director);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
      Director? director = new();

      try
      {
        director = _context.DirectorRepository.GetById(director => director.Id == id);
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
        _context.DirectorRepository.Delete(director);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return Ok(director);
    }
  }
}
