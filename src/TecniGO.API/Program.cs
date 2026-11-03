using Microsoft.EntityFrameworkCore;
using TecniGO.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar soporte para Controladores (Necesario para tus APIs)
builder.Services.AddControllers();

// 2. Configurar la Base de Datos con EF Core
builder.Services.AddDbContext<TecniGoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// COMENTADA temporalmente para evitar el error de redirección en desarrollo local:
// app.UseHttpsRedirection();

// 3. Mapear los controladores para que respondan a las peticiones HTTP
app.MapControllers();

app.Run();
