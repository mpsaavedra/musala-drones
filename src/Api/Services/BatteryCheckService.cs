using System.Text;
using Grpc.Net.Client;
using Musala.Drones.Api.Repositories;
using Musala.Drones.Domain.Models;
using Musala.Drones.Hub;

namespace Musala.Drones.Api.Services;

public interface IBatteryCheckService
{
    Task CheckBatteryLevels();
}

public class BatteryCheckService : IBatteryCheckService
{
    private readonly IDroneRepository _droneRepository;
    private readonly IBatteryAuditRepository _batteryRepository;
    private readonly ILogger<BatteryCheckService> _logger;
    private DronesHub.DronesHubClient _hubClient;
    private bool _connected = false;

    public BatteryCheckService(IDroneRepository droneRepository, IBatteryAuditRepository batteryRepository,
        ILogger<BatteryCheckService> logger)
    {
        _droneRepository = droneRepository;
        _batteryRepository = batteryRepository;
        _logger = logger;
    }
    
    public async Task CheckBatteryLevels()
    {
        var info = new StringBuilder();
        //info.Append("\tBattery levels summary from drones\n");
        //info.Append("  Id\tBattery capacity\n");
        foreach (var drone in _droneRepository.Query.ToList())
        {
            try
            {
                // get drone's  battery status 
                ConnectToHub();
                if (!_connected) return;
                var status = await _hubClient.GetDroneStatusAsync(new DroneStatusRequest { Id = drone.Id });
                await _batteryRepository.Create(new BatteryAudit
                {
                    Drone = drone, BatteryCapacity = status.Battery
                });
                var idx = drone.Id < 10 ? $" {drone.Id}" : $"{drone.Id}";
                info.Append($" {idx}\t{status.Battery}\n");
            }
            catch (Exception e)
            {
                //_logger.LogError($"Could not retrieve drones battery level: {e.Message}");
            }
        }
        _logger.LogInformation(info.ToString());
    }

    private void ConnectToHub()
    {
        if (!_connected)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:5027");
                _hubClient = new DronesHub.DronesHubClient(channel);
                _connected = true;
                _logger.LogInformation("Connected to Hub");
            }
            catch (Exception e)
            {
                //_logger.LogError($"Could not connect to Hub {e.Message}");
                _connected = false;
            }
        }
    }
}
