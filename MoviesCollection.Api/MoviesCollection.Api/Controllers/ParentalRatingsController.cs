using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/parentalratings")]
  [ApiController]
  public class ParentalRatingsController : ControllerBase
  {
    private readonly IUnitOfWork _context;

    public ParentalRatingsController(IUnitOfWork context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ParentalRating>> Get()
    {
      List<ParentalRating> parentalRatings = new();

      try
      {
        parentalRatings = _context.ParentalRatingRepository.Get().ToList();
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
    public ActionResult<ParentalRating> Get(int id)
    {
      ParentalRating? parentalRating = new();

      try
      {
        parentalRating = _context.ParentalRatingRepository.GetById(parentalRating => parentalRating.Id == id);
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
        _context.ParentalRatingRepository.Add(parentalRating);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return new CreatedAtRouteResult("GetParentalRating", new { id = parentalRating.Id }, parentalRating);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, ParentalRating parentalRating)
    {
      if (id != parentalRating.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }
      try
      {
        _context.ParentalRatingRepository.Update(parentalRating);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return Ok(parentalRating);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
      ParentalRating? parentalRating = new();

      try
      {
        parentalRating = _context.ParentalRatingRepository.GetById(parentalRating => parentalRating.Id == id);
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
        _context.ParentalRatingRepository.Delete(parentalRating);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(parentalRating);
    }
  }
}
