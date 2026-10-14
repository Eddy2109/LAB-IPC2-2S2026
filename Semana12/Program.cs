// dotnet new webapi -n NombreApi

using Semana12.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Mantener una sola instancia de la lista durante la ejecución.
builder.Services.AddSingleton<ListaProductos>();

var app = builder.Build();

app.MapControllers();

app.Run();
