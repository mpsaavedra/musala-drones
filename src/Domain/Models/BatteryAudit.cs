using Musala.Drones.BuildingBlocks;

namespace Musala.Drones.Domain.Models;

public class BatteryAudit : BusinessEntity
{
    public int DroneId { get; set; }
    
    public virtual Drone Drone { get; set; }
    
    public float BatteryCapacity { get; set; }
}