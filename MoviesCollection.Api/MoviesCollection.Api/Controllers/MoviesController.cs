﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public MoviesController(ApiDbContext context)
    {
      _context = context;
    }    

    [HttpGet]
    public ActionResult<IEnumerable<Movie>> Get()
    {
      List<Movie> movies = new();

      try
      {
        movies = _context.Movies.AsNoTracking().Include(g => g.Genre).Include(d => d.Director).Include(c => c.Country)
        .Include(l => l.Language).Include(p => p.ParentalRating).ToList();
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

    [HttpGet("{id:int}", Name = "GetMovie")]
    public ActionResult<Movie> Get(int id)
    {
      Movie? movie = new();

      try
      {
        movie = _context.Movies.AsNoTracking().FirstOrDefault(movie => movie.Id == id);
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
        _context.Movies.Add(movie);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetMovie", new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Movie movie)
    {
      if (id != movie.Id)
      {
        return BadRequest("Os id's são diferentes.");
      }

      try
      {
        _context.Entry(movie).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(movie);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      Movie? movie = new();

      try
      {
        movie = _context.Movies.AsNoTracking().FirstOrDefault(_movie => _movie.Id == id);
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
        _context.Movies.Remove(movie);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(movie);
    }
  }
}