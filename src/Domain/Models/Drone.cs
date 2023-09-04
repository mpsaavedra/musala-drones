using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Musala.Drones.BuildingBlocks;
using Musala.Drones.Domain.Enums;

namespace Musala.Drones.Domain.Models;

public class Drone : BusinessEntity
{
    [JsonPropertyName("serialNumber")]
    public string SerialNumber { get; set; }
    
    [JsonPropertyName("model")]
    public DroneModel Model { get; set; } = DroneModel.LightWeight;
    
    [JsonPropertyName("weightLimit")]
    public int WeightLimit { get; set; }
    
    [JsonPropertyName("batteryCapacity")]
    public int BatteryCapacity { get; set; }
    
    [JsonPropertyName("state")]
    public DroneState State { get; set; } = DroneState.Idle;

    public virtual ICollection<DroneCharge> DroneCharges { get; set; } = new HashSet<DroneCharge>();
}