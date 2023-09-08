using Musala.Drones.Contracts.Enums;

namespace Musala.Drones.Hub.Models;

public class Drone
{
    public int Id { get; set; }
    public float BatteryLevel { get; set; }
    
    public float WeightLimit { get; set; }
    public DroneState State { get; set; }

    public int DeliveringTime { get; set; } = 0;
}