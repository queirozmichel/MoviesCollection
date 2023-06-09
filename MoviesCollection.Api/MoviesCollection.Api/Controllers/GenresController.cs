﻿using AutoMapper;
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

    /// <summary>
    /// Obtém todos os gêneros
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDTO>>> Get([FromQuery] GenresParameters genresParameters)
    {
      PagedList<Genre> genres = new();
      List<GenreDTO> genresDTO = new();

      try
      {
        genres = await _context.GenreRepository.GetGenres(genresParameters);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (genres is null)
      {
        return NotFound("Gêneros não encontrados.");
      }

      var metadata = new
      {
        genres.TotalCount,
        genres.PageSize,
        genres.CurrentPage,
        genres.TotalPages,
        genres.HasNext,
        genres.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
      genresDTO = _mapper.Map<List<GenreDTO>>(genres);
      return genresDTO;
    }

    /// <summary>
    /// Obtém um gênero pelo seu ID
    /// </summary>
    [HttpGet("{id:int:min(1)}", Name = "GetGenre")]
    public async Task<ActionResult<GenreDTO>> Get(int id)
    {
      Genre? genre = new();
      GenreDTO genreDTO = new();
      try
      {
        genre = await _context.GenreRepository.GetById(genre => genre.Id == id);
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

    /// <summary>
    /// Inclui um novo gênero
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post(GenreDTO genreDto)
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
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      genreDTO = _mapper.Map<GenreDTO>(genre);
      return new CreatedAtRouteResult("GetGenre", new { id = genre.Id }, genreDTO);
    }

    /// <summary>
    /// Edita um gênero pelo seu ID
    /// </summary>
    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, Genre genreDto)
    {
      Genre genre = new();

      if (id != genreDto.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        genre = await _context.GenreRepository.GetById(x => x.Id == id);

        if (genre is null)
        {
          return NotFound($"Gênero com o id {id} não encontrado.");
        }
        else
        {
          genre = _mapper.Map<Genre>(genreDto);
        }

        _context.GenreRepository.Update(genre);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    /// <summary>
    /// Remove um gênero pelo seu ID
    /// </summary>
    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<GenreDTO>> Delete(int id)
    {
      Genre? genre = new();
      GenreDTO genreDTO = new();

      try
      {
        genre = await _context.GenreRepository.GetById(genre => genre.Id == id);
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
        await _context.Commit();
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
