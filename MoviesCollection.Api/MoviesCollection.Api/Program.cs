using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.DTOs.Mappings;
using MoviesCollection.Api.Helpers;
using MoviesCollection.Api.Repository;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); //Equivalente ao ConfigureServices()
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }); //AutoMapper
IMapper mapper = mappingConfig.CreateMapper(); //AutoMapper

// Add services to the container.
string? mySqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddCors(); // Adicionar Cors
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>(); //Unity Of Work
builder.Services.AddSingleton(mapper); //AutoMapper
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(mySqlServerConnection));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApiDbContext>().AddDefaultTokenProviders();

//JWT - Adiciona o manipulador de autenticação e define o esquema de autenticação Bearer. Valida o emirros, a audiência e a chave usando a chave secreta e por fim, valida a assisantura.
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
//Configuração do swagger para poder aceitar o JWT token.
builder.Services.AddSwaggerGen(x =>
{
  x.SwaggerDoc("v1", new OpenApiInfo { Title = "Acervo de Filmes", Version = "v1" });
  x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Header de autorização JWT usando o esquema Bearer. \r\n\r\nInforme 'Bearer'[espaço] e o seu token. \r\n\r\n Exemplo: \'Bearer 12345abcdef\'",
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
  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  x.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//Habilita um middleware para redirecionar para https
app.UseHttpsRedirection();
//Habilita o middleware responsável por permitir as requisições de outras origens
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
//Habilita o middleware que responsável pela autenticação
app.UseAuthentication();
//Habilita o middleware responsável pela autorização
app.UseAuthorization();

app.MapControllers();

app.Run();
