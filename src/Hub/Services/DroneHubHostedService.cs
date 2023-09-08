namespace Musala.Drones.Hub.Services;

public class DroneHubHostedService : BackgroundService
{
    private readonly ILogger<DroneHubHostedService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private TimeSpan _period = TimeSpan.FromSeconds(1);

    public DroneHubHostedService(ILogger<DroneHubHostedService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var _timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested &&
               await _timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await using var scope = _scopeFactory.CreateAsyncScope();
                var svr = scope.ServiceProvider.GetRequiredService<IDroneHubService>();
                await svr.ExecuteAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Fail to execute hub service message: {e.Message}");
            }
        }
    }
}