using CatalogoJogosAPI.Data;
using CatalogoJogosAPI.Data;
using CatalogoJogosAPI.Repositories;
using CatalogoJogosAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseSqlite("Data Source=catalogo.db"));

builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();

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