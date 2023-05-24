using Microsoft.AspNetCore.Mvc;
using MoviesCollection.Api.Models;
using MoviesCollection.Api.Repository;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/languages")]
  [ApiController]
  public class LanguagesController : ControllerBase
  {
    private readonly IUnitOfWork _context;

    public LanguagesController(IUnitOfWork context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Language>> Get()
    {
      List<Language> languages = new();

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

      return languages;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetLanguage")]
    public ActionResult<Language> Get(int id)
    {
      Language? language = new();
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

      return language;
    }

    [HttpPost]
    public ActionResult Post(Language language)
    {
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

      return new CreatedAtRouteResult("GetLanguage", new { id = language.Id }, language);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Language language)
    {
      if (id != language.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        _context.LanguageRepository.Update(language);
        _context.Commit();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(language);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
      Language? language = new();

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

      return Ok(language);
    }
  }
}
