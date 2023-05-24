using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/parentalratings")]
  [ApiController]
  public class ParentalRatingsController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public ParentalRatingsController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParentalRating>>> Get()
    {
      List<ParentalRating> parentalRatings = new();

      try
      {
        parentalRatings = await _context.ParentalRatings.AsNoTracking().ToListAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (parentalRatings is null)
      {
        return NotFound("Classificações indicativas não encontradas");
      }
      return parentalRatings;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetParentalRating")]
    public async Task<ActionResult<ParentalRating>> Get(int id)
    {
      ParentalRating? parentalRating = new();

      try
      {
        parentalRating = await _context.ParentalRatings.AsNoTracking().FirstOrDefaultAsync(parentalRating => parentalRating.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (parentalRating is null)
      {
        return NotFound($"Classificação indicativa com id {id} não encontrada");
      }
      return parentalRating;
    }

    [HttpPost]
    public async Task<ActionResult> Post(ParentalRating parentalRating)
    {
      if (parentalRating is null)
      {
        return BadRequest();
      }
      try
      {
        await _context.ParentalRatings.AddAsync(parentalRating);
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return new CreatedAtRouteResult("GetParentalRating", new { id = parentalRating.Id }, parentalRating);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, ParentalRating parentalRating)
    {
      if (id != parentalRating.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }
      try
      {
        _context.Entry(parentalRating).State = EntityState.Modified;
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return Ok(parentalRating);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult> Delete(int id)
    {
      ParentalRating? parentalRating = new();

      try
      {
        parentalRating = await _context.ParentalRatings.AsNoTracking().FirstOrDefaultAsync(parentalRating => parentalRating.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (parentalRating is null)
      {
        return NotFound($"Classificação indicativa com o id {id} não encontrada.");
      }

      try
      {
        _context.ParentalRatings.Remove(parentalRating);
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(parentalRating);
    }
  }
}
