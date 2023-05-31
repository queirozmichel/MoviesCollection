using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.DTOs;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;
using MoviesCollection.Api.Repository;
using System.Text.Json;

namespace MoviesCollection.Api.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [ApiConventionType(typeof(DefaultApiConventions))]
  [Produces("application/json")]
  [Route("api/parentalratings")]
  [ApiController]
  public class ParentalRatingsController : ControllerBase
  {
    private readonly IUnitOfWork _context;
    private IMapper _mapper;

    public ParentalRatingsController(IUnitOfWork context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Obtém todos os controles parentais
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParentalRatingDTO>>> Get([FromQuery] ParentalRatingsParameters parentalRatingsParameters)
    {
      PagedList<ParentalRating> parentalRatings = new();
      List<ParentalRatingDTO> parentalRatingsDTO = new();

      try
      {
        parentalRatings = await _context.ParentalRatingRepository.GetParentalRatings(parentalRatingsParameters);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (parentalRatings is null)
      {
        return NotFound("Classificações indicativas não encontradas");
      }

      var metadata = new
      {
        parentalRatings.TotalCount,
        parentalRatings.PageSize,
        parentalRatings.CurrentPage,
        parentalRatings.TotalPages,
        parentalRatings.HasNext,
        parentalRatings.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
      parentalRatingsDTO = _mapper.Map<List<ParentalRatingDTO>>(parentalRatings);
      return parentalRatingsDTO;
    }

    /// <summary>
    /// Obtém um controle parental pelo seu ID
    /// </summary>
    [HttpGet("{id:int:min(1)}", Name = "GetParentalRating")]
    public async Task<ActionResult<ParentalRatingDTO>> Get(int id)
    {
      ParentalRating? parentalRating = new();
      ParentalRatingDTO parentalRatingDTO = new();

      try
      {
        parentalRating = await _context.ParentalRatingRepository.GetById(parentalRating => parentalRating.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (parentalRating is null)
      {
        return NotFound($"Classificação indicativa com id {id} não encontrada");
      }

      parentalRatingDTO = _mapper.Map<ParentalRatingDTO>(parentalRating);
      return parentalRatingDTO;
    }

    /// <summary>
    /// Inclui um novo controle parental
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post(ParentalRatingDTO parentalRatingDto)
    {
      ParentalRating parentalRating = _mapper.Map<ParentalRating>(parentalRatingDto);
      ParentalRatingDTO parentalRatingDTO = new();

      if (parentalRating is null)
      {
        return BadRequest();
      }
      try
      {
        _context.ParentalRatingRepository.Add(parentalRating);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      parentalRatingDTO = _mapper.Map<ParentalRatingDTO>(parentalRating);
      return new CreatedAtRouteResult("GetParentalRating", new { id = parentalRating.Id }, parentalRatingDTO);
    }

    /// <summary>
    /// Edita um controle parental pelo seu ID
    /// </summary>
    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, ParentalRatingDTO parentalRatingDto)
    {
      ParentalRating parentalRating = new();

      if (id != parentalRatingDto.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        parentalRating = await _context.ParentalRatingRepository.GetById(x => x.Id == id);

        if (parentalRating is null)
        {
          return NotFound($"Classificação indicativa com id {id} não encontrada");
        }
        else
        {
          parentalRating = _mapper.Map<ParentalRating>(parentalRatingDto);
        }

        _context.ParentalRatingRepository.Update(parentalRating);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }
      return Ok();
    }

    /// <summary>
    /// Remove um controle parental pelo seu ID
    /// </summary>
    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<ParentalRatingDTO>> Delete(int id)
    {
      ParentalRating? parentalRating = new();
      ParentalRatingDTO parentalRatingDTO = new();

      try
      {
        parentalRating = await _context.ParentalRatingRepository.GetById(parentalRating => parentalRating.Id == id);
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
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      parentalRatingDTO = _mapper.Map<ParentalRatingDTO>(parentalRating);
      return Ok(parentalRatingDTO);
    }
  }
}
