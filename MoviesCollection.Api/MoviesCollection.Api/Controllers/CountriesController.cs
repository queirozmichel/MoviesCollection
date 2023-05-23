using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class CountriesController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public CountriesController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Country>> Get()
    {
      List<Country> countries = new();

      try
      {
        countries = _context.Countries.AsNoTracking().ToList();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (countries is null)
      {
        return NotFound("Países não foram encontrados.");
      }

      return countries;
    }

    [HttpGet("{id:int}", Name = "GetCountry")]
    public ActionResult<Country> Get(int id)
    {
      Country? country = new();

      try
      {
        country = _context.Countries.AsNoTracking().FirstOrDefault(country => country.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (country is null)
      {
        return NotFound($"País com o id {id} não encontrado.");
      }

      return country;
    }

    [HttpPost]
    public ActionResult Post(Country country)
    {
      if (country is null)
      {
        return BadRequest();
      }

      try
      {
        _context.Countries.Add(country);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetCountry", new { id = country.Id }, country);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Country country)
    {
      if (id != country.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        _context.Entry(country).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(country);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      Country? country = new();

      try
      {
        country = _context.Countries.AsNoTracking().FirstOrDefault(country => country.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (country is null)
      {
        return NotFound($"País com id {id} não encontrado");
      }

      try
      {
        _context.Countries.Remove(country);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(country);
    }
  }
}
