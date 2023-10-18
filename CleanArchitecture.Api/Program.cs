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

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;

    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    StreamerDbContext context = scope.ServiceProvider.GetRequiredService<StreamerDbContext>();

    await context.Database.MigrateAsync();

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
