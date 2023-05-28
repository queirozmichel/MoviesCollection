using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.DTOs;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;
using MoviesCollection.Api.Repository;
using System.Text.Json;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/directors")]
  [ApiController]
  public class DirectorsController : ControllerBase
  {
    private readonly IUnitOfWork _context;
    private IMapper _mapper;

    public DirectorsController(IUnitOfWork context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DirectorDTO>> Get([FromQuery] DirectorsParameters directorsParameters)
    {
      PagedList<Director>? directors = new();
      List<DirectorDTO> directorsDTO = new();

      try
      {
        directors = _context.DirectorRepository.GetDirectors(directorsParameters);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (directors is null)
      {
        return NotFound("Diretores não encontrados.");
      }

      var metadata = new
      {
        directors.TotalCount,
        directors.PageSize,
        directors.CurrentPage,
        directors.TotalPages,
        directors.HasNext,
        directors.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
      directorsDTO = _mapper.Map<List<DirectorDTO>>(directors);
      return directorsDTO;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetDirector")]
    public ActionResult<DirectorDTO> Get(int id)
    {
      Director? director = new();
      DirectorDTO directorDTO = new();

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

      directorDTO = _mapper.Map<DirectorDTO>(director);
      return directorDTO;
    }

    [HttpPost]
    public ActionResult Post(Director directorDto)
    {
      Director director = _mapper.Map<Director>(directorDto);
      DirectorDTO directorDTO = new();

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

      directorDTO = _mapper.Map<DirectorDTO>(director);
      return new CreatedAtRouteResult("GetDirector", new { id = director.Id }, directorDTO);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, DirectorDTO directorDto)
    {
      Director director = new();

      if (id != directorDto.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        director = _context.DirectorRepository.GetById(x => x.Id == id);

        if (director is null)
        {
          return NotFound($"Diretor com id {id} não encontrado.");
        }
        else
        {
          director = _mapper.Map<Director>(directorDto);
        }

        _context.DirectorRepository.Update(director);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<DirectorDTO> Delete(int id)
    {
      Director? director = new();
      DirectorDTO directorDTO = new();

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

      directorDTO = _mapper.Map<DirectorDTO>(director);
      return Ok(directorDTO);
    }
  }
}
