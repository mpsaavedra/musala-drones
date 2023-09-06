namespace Musala.Drones.Api.Services;

public class BatteryCheckHostedService : BackgroundService
{
    private readonly ILogger<BatteryCheckHostedService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private TimeSpan _period = TimeSpan.FromSeconds(10);

    public BatteryCheckHostedService(ILogger<BatteryCheckHostedService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        IsEnable = true;
    }
    
    public bool IsEnable { get; set; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var _timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested && await _timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (IsEnable)
                {
                    await using var asyncScope = _scopeFactory.CreateAsyncScope();
                    var svr = asyncScope.ServiceProvider.GetRequiredService<IBatteryCheckService>();
                    await svr.CheckBatteryLevels();
                    _logger.LogInformation($"Battery check service executed");
                }
                else
                {
                    _logger.LogInformation("Battery check service is stopped");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Fail to execute Battery check service with message: {e.Message}");
            }
        }
    }
}