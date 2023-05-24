using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/movies")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly IUnitOfWork _context;

    public MoviesController(IUnitOfWork context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Movie>> Get()
    {
      List<Movie> movies = new();

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

      return movies;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetMovie")]
    public ActionResult<Movie> Get(int id)
    {
      Movie? movie = new();

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

      return Ok(movie);
    }

    [HttpPost]
    public ActionResult Post(Movie movie)
    {
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

      return new CreatedAtRouteResult("GetMovie", new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Movie movie)
    {
      if (id != movie.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.MovieRepository.Update(movie);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(movie);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
      Movie? movie = new();

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

      return Ok(movie);
    }
  }
}
