using Musala.Drones.Domain.Enums;

namespace Musala.Drones.Domain.Dtos.Responses;

public class AvailableDronesResponse : BaseResponse
{
    public ICollection<AvailableDrones> AvailableDrones { get; set; } = new HashSet<AvailableDrones>();
}

public class AvailableDrones
{
    public string SerialNumber { get; set; }
    
    public float BatteryCapacity { get; set; }
    
    public float WeightLimit { get; set; }
    
    public DroneModel Model { get; set; }
    
    public DroneState State { get; set; }
}