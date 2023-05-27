using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.DTOs;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
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

    [HttpGet]
    public ActionResult<IEnumerable<MovieDTO>> Get()
    {
      List<Movie> movies = new();
      List<MovieDTO> moviesDTO = new();

      try
      {
        movies = _context.MovieRepository.Get().ToList();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (movies is null)
      {
        return NotFound("Filmes não foram encontrados.");
      }

      moviesDTO = _mapper.Map<List<MovieDTO>>(movies);
      return moviesDTO;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetMovie")]
    public ActionResult<MovieDTO> Get(int id)
    {
      Movie? movie = new();
      MovieDTO movieDTO = new();

      try
      {
        movie = _context.MovieRepository.GetById(movie => movie.Id == id);
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

    [HttpPost]
    public ActionResult Post(MovieDTO movieDto)
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
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      movieDTO = _mapper.Map<MovieDTO>(movie);
      return new CreatedAtRouteResult("GetMovie", new { id = movie.Id }, movieDTO);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, MovieDTO movieDto)
    {
      Movie movie = new();

      if (id != movieDto.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        movie = _context.MovieRepository.GetById(x => x.Id == id);

        if (movie is null)
        {
          return NotFound($"Filme com id {id} não encontrado.");
        }
        else
        {
          movie = _mapper.Map<Movie>(movieDto);
        }

        _context.MovieRepository.Update(movie);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<MovieDTO> Delete(int id)
    {
      Movie? movie = new();
      MovieDTO movieDTO = new();

      try
      {
        movie = _context.MovieRepository.GetById(_movie => _movie.Id == id);
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
        _context.Commit();
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
