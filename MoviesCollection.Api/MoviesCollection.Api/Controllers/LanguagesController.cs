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
  [Route("api/languages")]
  [ApiController]
  public class LanguagesController : ControllerBase
  {
    private readonly IUnitOfWork _context;
    private IMapper _mapper;

    public LanguagesController(IUnitOfWork context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Obtém todos os idiomas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LanguageDTO>>> Get([FromQuery] LanguageParameters languageParameters)
    {
      PagedList<Language> languages = new();
      List<LanguageDTO> languagesDTO = new();

      try
      {
        languages = await _context.LanguageRepository.GetLanguages(languageParameters);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (languages is null)
      {
        return NotFound("Idiomas não encontrados.");
      }

      var metadata = new
      {
        languages.TotalCount,
        languages.PageSize,
        languages.CurrentPage,
        languages.TotalPages,
        languages.HasNext,
        languages.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
      languagesDTO = _mapper.Map<List<LanguageDTO>>(languages);
      return languagesDTO;
    }

    /// <summary>
    /// Obtém um idioma pelo seu ID
    /// </summary>
    [HttpGet("{id:int:min(1)}", Name = "GetLanguage")]
    public async Task<ActionResult<LanguageDTO>> Get(int id)
    {
      Language? language = new();
      LanguageDTO languageDTO = new();

      try
      {
        language = await _context.LanguageRepository.GetById(language => language.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (language is null)
      {
        return NotFound($"Idioma com o id {id} não encontrado.");
      }

      languageDTO = _mapper.Map<LanguageDTO>(language);
      return languageDTO;
    }

    /// <summary>
    /// Inclui um novo idioma
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post(LanguageDTO languageDto)
    {
      Language language = _mapper.Map<Language>(languageDto);
      LanguageDTO languageDTO = new();

      if (language is null)
      {
        return BadRequest();
      }

      try
      {
        _context.LanguageRepository.Add(language);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      languageDTO = _mapper.Map<LanguageDTO>(language);
      return new CreatedAtRouteResult("GetLanguage", new { id = language.Id }, languageDTO);
    }

    /// <summary>
    /// Edita um idioma pelo seu ID
    /// </summary>
    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, LanguageDTO languageDto)
    {
      Language language = new();

      if (id != languageDto.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        language = await _context.LanguageRepository.GetById(x => x.Id == id);

        if (language is null)
        {
          return NotFound($"Idioma com o id {id} não encontrado.");
        }
        else
        {
          language = _mapper.Map<Language>(languageDto);
        }

        _context.LanguageRepository.Update(language);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    /// <summary>
    /// Remove um idioma pelo seu ID
    /// </summary>
    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<LanguageDTO>> Delete(int id)
    {
      Language? language = new();
      LanguageDTO languageDTO = new();

      try
      {
        language = await _context.LanguageRepository.GetById(language => language.Id == id);
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (language is null)
      {
        return NotFound($"Idioma com o id {id} não encontrado");
      }

      try
      {
        _context.LanguageRepository.Delete(language);
        await _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      languageDTO = _mapper.Map<LanguageDTO>(language);
      return Ok(languageDTO);
    }
  }
}
