using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.DTOs.Mappings;
using MoviesCollection.Api.Helpers;
using MoviesCollection.Api.Repository;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); //Equivalente ao ConfigureServices()
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }); //AutoMapper
IMapper mapper = mappingConfig.CreateMapper(); //AutoMapper

// Add services to the container.
string? mySqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>(); //Unity Of Work
builder.Services.AddSingleton(mapper); //AutoMapper
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(mySqlServerConnection));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApiDbContext>().AddDefaultTokenProviders();

//JWT - Adiciona o manipulador de autentica��o e define o esquema de autentica��o Bearer. Valida o emirros, a audi�ncia e a chave usando a chave secreta e por fim, valida a assisantura.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
  ValidateIssuer = true,
  ValidateAudience = true,
  ValidateLifetime = true,
  ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
  ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
  ValidateIssuerSigningKey = true,
  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
  options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.AddEndpointsApiExplorer();
//Configura��o do swagger para poder aceitar o JWT token.
builder.Services.AddSwaggerGen(x =>
{
  x.SwaggerDoc("v1", new OpenApiInfo { Title = "MoviesCollection.Api", Version = "v1" });
  x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Header de autoriza��o JWT usando o esquema Bearer. \r\n\r\nInforme 'Bearer'[espa�o] e o seu token. \r\n\r\n Exemplo: \'Bearer 12345abcdef\'",
  });
  x.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      new string[]{}
    }
  });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Adiciona um middleware para redirecionar para https
app.UseHttpsRedirection();

//Adiciona o middleware que habilita a autentica��o
app.UseAuthentication();

//Adiciona o middleware que habilita a autoriza��o
app.UseAuthorization();

app.MapControllers();

app.Run();
