using Musala.Drones.Api.Repositories;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Services;

public interface IBatteryCheckService
{
    Task CheckBatteryLevels();
}

public class BatteryCheckService : IBatteryCheckService
{
    private readonly IDroneRepository _droneRepository;
    private readonly IBatteryAuditRepository _batteryRepository;

    public BatteryCheckService(IDroneRepository droneRepository, IBatteryAuditRepository batteryRepository)
    {
        _droneRepository = droneRepository;
        _batteryRepository = batteryRepository;
    }
    
    public async Task CheckBatteryLevels()
    {
        foreach (var drone in _droneRepository.Query.ToList())
        {
            await _batteryRepository.Create(new BatteryAudit
            {
                Drone = drone, BatteryCapacity = drone.BatteryCapacity
            });
        }
    }
}