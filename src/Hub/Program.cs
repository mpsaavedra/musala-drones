using Microsoft.Net.Http.Headers;
using Musala.Drones.Hub;
using Musala.Drones.Hub.Services;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.Configuration.Get<AppSettings>();
builder.Services
    .AddSingleton<IDroneHubService, DroneHubService>()
    .AddHostedService<DroneHubHostedService>()
    .AddHttpClient<IApiClient, ApiClient>(client =>
    {
        client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        client.BaseAddress = new Uri(appSettings.Urls.Http);
    });
builder.Services.AddHostedService(prov => prov.GetRequiredService<DroneHubHostedService>());

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcDroneService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
