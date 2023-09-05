using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Musala.Drones.Api.Data;
using Musala.Drones.Api.Repositories;
using Musala.Drones.Api.Services;
using Musala.Drones.Api.Settings;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.Configuration.Get<AppSettings>();
var inMemoryDb = builder.Configuration.GetConnectionString("DefaultConnection");
var postgresDb = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
{
    if (appSettings.UseInMemoryDb)
        cfg.EnableSensitiveDataLogging().UseInMemoryDatabase(inMemoryDb);
    else
        cfg.EnableSensitiveDataLogging().UseNpgsql(postgresDb, opts =>
        {
            opts.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            opts.EnableRetryOnFailure(10, TimeSpan.FromSeconds(10.0), null);
        });
});
builder.Services.AddAutoMapper(typeof(Program));
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

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

DbSeeder.SeedData(app.Services);

app.Run();
