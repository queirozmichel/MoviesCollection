using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.DTOs;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
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

    [HttpGet]
    public ActionResult<IEnumerable<LanguageDTO>> Get()
    {
      List<Language> languages = new();
      List<LanguageDTO> languagesDTO = new();

      try
      {
        languages = _context.LanguageRepository.Get().ToList();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      if (languages is null)
      {
        return NotFound("Idiomas não encontrados.");
      }

      languagesDTO = _mapper.Map<List<LanguageDTO>>(languages);
      return languagesDTO;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetLanguage")]
    public ActionResult<LanguageDTO> Get(int id)
    {
      Language? language = new();
      LanguageDTO languageDTO = new();

      try
      {
        language = _context.LanguageRepository.GetById(language => language.Id == id);
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

    [HttpPost]
    public ActionResult Post(LanguageDTO languageDto)
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
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      languageDTO = _mapper.Map<LanguageDTO>(language);
      return new CreatedAtRouteResult("GetLanguage", new { id = language.Id }, languageDTO);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, LanguageDTO languageDto)
    {
      Language language = new();

      if (id != languageDto.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        language = _context.LanguageRepository.GetById(x => x.Id == id);

        if (language is null)
        {
          return NotFound($"Idioma com o id {id} não encontrado.");
        }
        else
        {
          language = _mapper.Map<Language>(languageDto);
        }

        _context.LanguageRepository.Update(language);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok();
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<LanguageDTO> Delete(int id)
    {
      Language? language = new();
      LanguageDTO languageDTO= new();

      try
      {
        language = _context.LanguageRepository.GetById(language => language.Id == id);
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
        _context.Commit();
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
