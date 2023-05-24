using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/directors")]
  [ApiController]
  public class DirectorsController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public DirectorsController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Director>>> Get()
    {
      List<Director>? directors = new();

      try
      {
        directors = await _context.Directors.AsNoTracking().ToListAsync();
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
    public async Task<ActionResult<Director>> Get(int id)
    {
      Director? director = new();
      try
      {
        director = await _context.Directors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
    public async Task<ActionResult> Post(Director director)
    {
      if (director is null)
      {
        return BadRequest();
      }

      try
      {
        await _context.AddAsync(director);
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetDirector", new { id = director.Id }, director);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, Director director)
    {
      if (id != director.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.Entry(director).State = EntityState.Modified;
       await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(director);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult> Delete(int id)
    {
      Director? director = new();

      try
      {
        director = await _context.Directors.AsNoTracking().FirstOrDefaultAsync(director => director.Id == id);
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
       await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return Ok(director);
    }
  }
}
