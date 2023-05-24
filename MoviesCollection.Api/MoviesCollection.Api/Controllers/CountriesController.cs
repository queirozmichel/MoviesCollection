using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/countries")]
  [ApiController]
  public class CountriesController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public CountriesController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> Get()
    {
      List<Country> countries = new();

      try
      {
        countries = await _context.Countries.AsNoTracking().ToListAsync();
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
    public async Task<ActionResult<Country>> Get(int id)
    {
      Country? country = new();

      try
      {
        country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(country => country.Id == id);
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
    public async Task<ActionResult> Post(Country country)
    {
      if (country is null)
      {
        return BadRequest();
      }

      try
      {
       await _context.Countries.AddAsync(country);
       await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetCountry", new { id = country.Id }, country);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, Country country)
    {
      if (id != country.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        _context.Entry(country).State = EntityState.Modified;
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(country);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult> Delete(int id)
    {
      Country? country = new();

      try
      {
        country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(country => country.Id == id);
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
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(country);
    }
  }
}
