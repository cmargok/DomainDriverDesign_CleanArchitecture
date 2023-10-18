using CleanArchitecture.Application.Configuration;
using CleanArchitecture.Infrastructure.Configuration;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastrctureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//creamos el scope
using (var scope = app.Services.CreateScope())
{
    //obtenemos los servicios
    var service = scope.ServiceProvider;

    //obtenemos la facotory de loggers
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    //obtenemos el contexto o DBContext de la applicacion
    StreamerDbContext context = scope.ServiceProvider.GetRequiredService<StreamerDbContext>();

    //hace un update-database automatico, si hay migraciones pendientes, si no hay pendientes, no hace nada
    await context.Database.MigrateAsync();

    //llamamos al metodo estatico para sembrar la dataSS
    await StreamerDbContextSeed.SeedAsync(context, loggerFactory.CreateLogger<StreamerDbContextSeed>());

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
