using Musala.Drones.Contracts.Enums;

namespace Musala.Drones.Contracts.Dtos.Responses;

public class AvailableDronesResponse : BaseResponse
{
    public ICollection<AvailableDrones> AvailableDrones { get; set; } = new HashSet<AvailableDrones>();
}

public class AvailableDrones
{
    public int Id { get; set; }
    
    public string SerialNumber { get; set; }
    
    public float BatteryCapacity { get; set; }
    
    public float WeightLimit { get; set; }
    
    public DroneModel Model { get; set; }
    
    public DroneState State { get; set; }
}