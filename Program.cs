using CatalogoJogosAPI.Data;
using CatalogoJogosAPI.Repositories;
using CatalogoJogosAPI.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do Banco de Dados
builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseSqlite("Data Source=catalogo.db"));


builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();