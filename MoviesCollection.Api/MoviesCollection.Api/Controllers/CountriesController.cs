using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.DTOs;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Pagination;
using MoviesCollection.Api.Repository;
using System.Text.Json;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/countries")]
  [ApiController]
  public class CountriesController : ControllerBase
  {
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;

    public CountriesController(IUnitOfWork context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CountryDTO>> Get([FromQuery] CountriesParameters countriesParameters)
    {
      PagedList<Country> countries = new();
      List<CountryDTO> countriesDTO = new();

      try
      {
        countries = _context.CountryRepository.GetCountries(countriesParameters);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (countries is null)
      {
        return NotFound("Países não foram encontrados.");
      }

      var metadata = new
      {
        countries.TotalCount,
        countries.PageSize,
        countries.CurrentPage,
        countries.TotalPages,
        countries.HasNext,
        countries.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
      countriesDTO = _mapper.Map<List<CountryDTO>>(countries);
      return countriesDTO;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCountry")]
    public ActionResult<CountryDTO> Get(int id)
    {
      Country? country = new();
      CountryDTO? countryDTO = new();

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

      countryDTO = _mapper.Map<CountryDTO>(country);
      return countryDTO;
    }

    [HttpPost]
    public ActionResult Post(CountryDTO countryDto)
    {
      Country country = _mapper.Map<Country>(countryDto);
      CountryDTO countryDTO = new();

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

      countryDTO = _mapper.Map<CountryDTO>(country);
      return new CreatedAtRouteResult("GetCountry", new { id = country.Id }, countryDTO);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, CountryDTO countryDto)
    {
      Country country = new();

      if (id != countryDto.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        country = _context.CountryRepository.GetById(x => x.Id == id);

        if (country is null)
        {
          return NotFound($"País com o id {id} não encontrado.");
        }
        else
        {
          country = _mapper.Map<Country>(countryDto);
        }

        _context.CountryRepository.Update(country);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<CountryDTO> Delete(int id)
    {
      Country? country = new();
      CountryDTO countryDTO = new();

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

      countryDTO = _mapper.Map<CountryDTO>(country);
      return Ok(countryDTO);
    }
  }
}
