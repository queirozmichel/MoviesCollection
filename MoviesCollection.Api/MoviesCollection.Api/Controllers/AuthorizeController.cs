using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoviesCollection.Api.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesCollection.Api.Controllers
{
  [Route("api/authorize")]
  [ApiController]
  public class AuthorizeController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthorizeController(UserManager<IdentityUser> userManger, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
      _userManger = userManger;
      _signInManager = signInManager;
      _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
      return "AuthorizeController - Acessado em " + DateTime.Now.ToLongDateString();
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] UserDTO userDto)
    {
      var user = new IdentityUser
      {
        UserName = userDto.UserName
      };

      var result = await _userManger.CreateAsync(user, userDto.Password);

      if (!result.Succeeded)
      {
        return BadRequest(result.Errors);
      }

      await _signInManager.SignInAsync(user, false);
      return Ok(GenerateToken(userDto));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserDTO userDto)
    {
      var result = await _signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, isPersistent: false, lockoutOnFailure: false);

      if (result.Succeeded)
      {
        return Ok(GenerateToken(userDto));
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Login Inválido!");
        return BadRequest(ModelState);
      }
    }

    private UserTokenDTO GenerateToken(UserDTO userDto)
    {
      //Define as declarações do usuário.
      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.UniqueName, userDto.UserName),
        new Claim("meuPet", "Negrinha"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      //Gera uma chave privada com base em um algoritmo simétrico.
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

      //Gera a assinatura digital do token usando o algoritmo HmacSha256 e a chave privada.
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      //Define o tempo de expiração do token.
      var expires = _configuration["TokenConfiguration:ExpireHours"];
      var expiration = DateTime.UtcNow.AddHours(double.Parse(expires));

      //Geração do token.
      JwtSecurityToken token = new JwtSecurityToken
      (
        issuer: _configuration["TokenConfiguration:Issuer"],
        audience: _configuration["TokenConfiguration:Audience"],
        claims: claims,
        expires: expiration,
        signingCredentials: credentials
      );

      return new UserTokenDTO()
      {
        Authenticated = true,
        Token = new JwtSecurityTokenHandler().WriteToken(token),
        Expiration = expiration,
        Message = "Token JWT gerado com sucesso!"
      };
    }
  }
}
