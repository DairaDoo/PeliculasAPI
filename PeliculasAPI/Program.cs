using Microsoft.EntityFrameworkCore;
using PeliculasAPI;
using PeliculasAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60); // se guarda el caché
});

var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(",");

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders("cantidad-total-registros");
    });
});

// Registramos servicios para subir imagenes en Azure Storage Account
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();
