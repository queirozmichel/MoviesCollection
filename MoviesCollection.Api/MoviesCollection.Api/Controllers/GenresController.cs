using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.DTOs;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/genres")]
  [ApiController]
  public class GenresController : ControllerBase
  {
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;

    public GenresController(IUnitOfWork context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<GenreDTO>> Get()
    {
      List<Genre> genres = new();
      List<GenreDTO> genresDTO = new();

      try
      {
        genres = _context.GenreRepository.Get().ToList();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (genres is null)
      {
        return NotFound("Gêneros não encontrados.");
      }

      genresDTO = _mapper.Map<List<GenreDTO>>(genres);
      return genresDTO;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetGenre")]
    public ActionResult<GenreDTO> Get(int id)
    {
      Genre? genre = new();
      GenreDTO genreDTO = new();
      try
      {
        genre = _context.GenreRepository.GetById(genre => genre.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (genre is null)
      {
        return NotFound($"Gênero com o id {id} não encontrado.");
      }

      genreDTO = _mapper.Map<GenreDTO>(genre);
      return genreDTO;
    }

    [HttpPost]
    public ActionResult Post(GenreDTO genreDto)
    {
      Genre genre = _mapper.Map<Genre>(genreDto);
      GenreDTO genreDTO = new();

      if (genre is null)
      {
        return BadRequest();
      }

      try
      {
        _context.GenreRepository.Add(genre);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      genreDTO = _mapper.Map<GenreDTO>(genre);
      return new CreatedAtRouteResult("GetGenre", new { id = genre.Id }, genreDTO);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Genre genreDto)
    {
      Genre genre = new();

      if (id != genreDto.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        genre = _context.GenreRepository.GetById(x => x.Id == id);

        if (genre is null)
        {
          return NotFound($"Gênero com o id {id} não encontrado.");
        }
        else
        {
          genre = _mapper.Map<Genre>(genreDto);
        }

        _context.GenreRepository.Update(genre);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<GenreDTO> Delete(int id)
    {
      Genre? genre = new();
      GenreDTO genreDTO = new();

      try
      {
        genre = _context.GenreRepository.GetById(genre => genre.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (genre is null)
      {
        return NotFound($"Gênero com id {id} não encontrado");
      }

      try
      {
        _context.GenreRepository.Delete(genre);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      genreDTO = _mapper.Map<GenreDTO>(genre);
      return Ok(genreDTO);
    }
  }
}
