using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ParentalRatingsController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public ParentalRatingsController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ParentalRating>> Get()
    {
      List<ParentalRating> parentalRatings = new();

      try
      {
        parentalRatings = _context.ParentalRatings.AsNoTracking().ToList();
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

    [HttpGet("{id:int}", Name = "GetParentalRating")]
    public ActionResult<ParentalRating> Get(int id)
    {
      ParentalRating? parentalRating = new();

      try
      {
        parentalRating = _context.ParentalRatings.AsNoTracking().FirstOrDefault(parentalRating => parentalRating.Id == id);
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
    public ActionResult Post(ParentalRating parentalRating)
    {
      if (parentalRating is null)
      {
        return BadRequest();
      }
      try
      {
        _context.ParentalRatings.Add(parentalRating);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return new CreatedAtRouteResult("GetParentalRating", new { id = parentalRating.Id }, parentalRating);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, ParentalRating parentalRating)
    {
      if (id != parentalRating.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }
      try
      {
        _context.Entry(parentalRating).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return Ok(parentalRating);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      ParentalRating? parentalRating = new();

      try
      {
        parentalRating = _context.ParentalRatings.AsNoTracking().FirstOrDefault(parentalRating => parentalRating.Id == id);
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
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(parentalRating);
    }
  }
}
