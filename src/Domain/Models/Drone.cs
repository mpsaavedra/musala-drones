using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Musala.Drones.BuildingBlocks;
using Musala.Drones.Contracts.Enums;

namespace Musala.Drones.Domain.Models;

public class Drone : BusinessEntity
{
    [JsonPropertyName("serialNumber")]
    [MaxLength(100)]
    public string SerialNumber { get; set; }
    
    [JsonPropertyName("model")]
    public DroneModel Model { get; set; } = DroneModel.LightWeight;
    
    [JsonPropertyName("weightLimit")]
    [Range(1, 500)]
    public float WeightLimit { get; set; }
    
    [JsonPropertyName("batteryCapacity")]
    [Range(1, 100)]
    public float BatteryCapacity { get; set; }
    
    [JsonPropertyName("state")]
    public DroneState State { get; set; } = DroneState.Idle;

    public virtual ICollection<DroneCharge> DroneCharges { get; set; } = new HashSet<DroneCharge>();

    public virtual ICollection<BatteryAudit> BatteryAudits { get; set; } = new HashSet<BatteryAudit>();
}