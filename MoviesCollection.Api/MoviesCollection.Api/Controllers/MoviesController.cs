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
  [Route("api/movies")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly IUnitOfWork _context;
    private IMapper _mapper;

    public MoviesController(IUnitOfWork context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Obtém todos os filmes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> Get([FromQuery] MoviesParameters moviesParameters)
    {
      PagedList<Movie> movies = new();
      List<MovieDTO> moviesDTO = new();

      try
      {
        movies = await _context.MovieRepository.Get(moviesParameters);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (movies is null)
      {
        return NotFound("Filmes não foram encontrados.");
      }

      var metadata = new
      {
        movies.TotalCount,
        movies.PageSize,
        movies.CurrentPage,
        movies.TotalPages,
        movies.HasNext,
        movies.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
      moviesDTO = _mapper.Map<List<MovieDTO>>(movies);
      return moviesDTO;
    }

    /// <summary>
    /// Obtém um filme pelo seu ID
    /// </summary>
    [HttpGet("{id:int:min(1)}", Name = "GetMovie")]
    public async Task<ActionResult<MovieDTO>> Get(int id)
    {
      Movie? movie = new();
      MovieDTO movieDTO = new();

      try
      {
        movie = await _context.MovieRepository.GetById(movie => movie.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (movie is null)
      {
        return NotFound($"Filme com id {id} não encontrado.");
      }

      movieDTO = _mapper.Map<MovieDTO>(movie);
      return movieDTO;
    }

    /// <summary>
    /// Inclui um novo filme
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post(MovieDTO movieDto)
    {
      Movie movie = _mapper.Map<Movie>(movieDto);
      MovieDTO movieDTO = new();

      if (movie == null)
      {
        return BadRequest();
      }

      try
      {
        _context.MovieRepository.Add(movie);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      movieDTO = _mapper.Map<MovieDTO>(movie);
      return new CreatedAtRouteResult("GetMovie", new { id = movie.Id }, movieDTO);
    }

    /// <summary>
    /// Edita um filme pelo seu ID
    /// </summary>
    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, MovieDTO movieDto)
    {
      Movie movie = new();

      if (id != movieDto.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        movie = await _context.MovieRepository.GetById(x => x.Id == id);

        if (movie is null)
        {
          return NotFound($"Filme com id {id} não encontrado.");
        }
        else
        {
          movie = _mapper.Map<Movie>(movieDto);
        }

        _context.MovieRepository.Update(movie);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    /// <summary>
    /// Remove um filme pelo seu ID
    /// </summary>
    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<MovieDTO>> Delete(int id)
    {
      Movie? movie = new();
      MovieDTO movieDTO = new();

      try
      {
        movie = await _context.MovieRepository.GetById(_movie => _movie.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (movie is null)
      {
        return NotFound($"Filme com o id {id} não encontrado.");
      }

      try
      {
        _context.MovieRepository.Delete(movie);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      movieDTO = _mapper.Map<MovieDTO>(movie);
      return Ok(movieDTO);
    }
  }
}
