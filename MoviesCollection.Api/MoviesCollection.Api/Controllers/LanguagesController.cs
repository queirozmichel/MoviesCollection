using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class LanguagesController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public LanguagesController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Language>> Get()
    {
      List<Language> languages = new();

      try
      {
        languages = _context.Languages.AsNoTracking().ToList();
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

    [HttpGet("{id:int}", Name = "GetLanguage")]
    public ActionResult<Language> Get(int id)
    {
      Language? language = new();
      try
      {
        language = _context.Languages.AsNoTracking().FirstOrDefault(language => language.Id == id);
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
        _context.Languages.Add(language);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetLanguage", new { id = language.Id }, language);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Language language)
    {
      if (id != language.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        _context.Entry(language).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(language);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      Language? language = new();

      try
      {
        language = _context.Languages.AsNoTracking().FirstOrDefault(language => language.Id == id);
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
        _context.Languages.Remove(language);
        _context.SaveChanges();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(language);
    }
  }
}
