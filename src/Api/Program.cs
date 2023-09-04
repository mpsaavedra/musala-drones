using Microsoft.EntityFrameworkCore;
using Musala.Drones.Api.Data;
using Musala.Drones.Api.Repositories;
using Musala.Drones.Api.Services;
using Musala.Drones.Api.Settings;
using Musala.Drones.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
var dbSettings = builder.Configuration.Get<DbSettings>();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
{
    if (dbSettings.UseInMemory)
    {
        cfg.UseInMemoryDatabase(dbSettings.InMemoryDbName);
    }
    else
    {
        cfg.UseNpgsql(dbSettings.PostgreSqlConnectionString);
    }
});
builder.Services
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddTransient<IDroneRepository, DroneRepository>()
    .AddTransient<IDroneChargeRepository, DroneChargeRepository>()
    .AddTransient<IMedicationRepository, MedicationRepository>()
    .AddTransient<IMedicationChargeRepository, MedicationChargeRepository>()
    .AddTransient<IDispatcherService, DispatcherService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
