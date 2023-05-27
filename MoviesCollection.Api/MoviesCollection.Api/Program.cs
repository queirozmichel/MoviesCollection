using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesCollection.Api.Context;
using MoviesCollection.Api.DTOs.Mappings;
using MoviesCollection.Api.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); //Equivalente ao ConfigureServices()
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }); //AutoMapper
IMapper mapper = mappingConfig.CreateMapper(); //AutoMapper

// Add services to the container.
string? mySqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>(); //Unity Of Work
builder.Services.AddSingleton(mapper); //AutoMapper
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(mySqlServerConnection));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Adiciona um middleware para redirecionar para https
app.UseHttpsRedirection();

//Adiciona o middleware que habilita a autorização
app.UseAuthorization();

app.MapControllers();

app.Run();
