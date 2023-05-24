using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.Models;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/languages")]
  [ApiController]
  public class LanguagesController : ControllerBase
  {
    private readonly ApiDbContext _context;

    public LanguagesController(ApiDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Language>>> Get()
    {
      List<Language> languages = new();

      try
      {
        languages = await _context.Languages.AsNoTracking().ToListAsync();
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
    public async Task<ActionResult<Language>> Get(int id)
    {
      Language? language = new();
      try
      {
        language = await _context.Languages.AsNoTracking().FirstOrDefaultAsync(language => language.Id == id);
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
    public async Task<ActionResult> Post(Language language)
    {
      if (language is null)
      {
        return BadRequest();
      }
      try
      {
        await _context.Languages.AddAsync(language);
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return new CreatedAtRouteResult("GetLanguage", new { id = language.Id }, language);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, Language language)
    {
      if (id != language.Id)
      {
        return BadRequest("Os id's são diferentes");
      }

      try
      {
        _context.Entry(language).State = EntityState.Modified;
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(language);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult> Delete(int id)
    {
      Language? language = new();

      try
      {
        language = await _context.Languages.AsNoTracking().FirstOrDefaultAsync(language => language.Id == id);
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
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
      }

      return Ok(language);
    }
  }
}
