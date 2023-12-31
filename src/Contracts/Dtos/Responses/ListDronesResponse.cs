using Musala.Drones.Contracts.Enums;

namespace Musala.Drones.Contracts.Dtos.Responses;

public class ListDronesResponse : BaseResponse
{
    public ICollection<ListDrone> Drones { get; set; } = new HashSet<ListDrone>();
}

public class ListDrone
{
    public int Id { get; set; }
    
    public string SerialNumber { get; set; }
    
    public float BatteryCapacity { get; set; }
    
    public float WeightLimit { get; set; }
    
    public DroneModel Model { get; set; }
    
    public DroneState State { get; set; }
}