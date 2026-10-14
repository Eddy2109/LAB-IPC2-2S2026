var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddSingleton<EjemploProyecto3.Data.ConexionBD>();
builder.Services.AddSingleton<EjemploProyecto3.Services.NitService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

