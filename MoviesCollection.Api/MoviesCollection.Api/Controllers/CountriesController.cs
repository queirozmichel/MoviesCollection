using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/countries")]
  [ApiController]
  public class CountriesController : ControllerBase
  {
    private readonly IUnitOfWork _context;

    public CountriesController(IUnitOfWork context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Country>> Get()
    {
      List<Country> countries = new();

      try
      {
        countries = _context.CountryRepository.Get().ToList();
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

    [HttpGet("{id:int:min(1)}", Name = "GetCountry")]
    public ActionResult<Country> Get(int id)
    {
      Country? country = new();

      try
      {
        country = _context.CountryRepository.GetById(country => country.Id == id);
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
       _context.CountryRepository.Add(country);
       _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetCountry", new { id = country.Id }, country);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Country country)
    {
      if (id != country.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        _context.CountryRepository.Update(country);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(country);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
      Country? country = new();

      try
      {
        country = _context.CountryRepository.GetById(country => country.Id == id);
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
        _context.CountryRepository.Delete(country);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(country);
    }
  }
}
