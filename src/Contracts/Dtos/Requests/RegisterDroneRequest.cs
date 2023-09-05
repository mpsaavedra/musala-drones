using Musala.Drones.Contracts.Enums;

namespace Musala.Drones.Contracts.Dtos.Requests;

public class RegisterDroneRequest
{
    public string SerialNumber { get; set; }
    
    public DroneModel Model { get; set; }
    
    public float WeightLimit { get; set; }
    
    public float BatteryCapacity { get; set; }
    
    public DroneState State { get; set; }
}