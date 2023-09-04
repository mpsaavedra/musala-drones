using System.Text.Json.Serialization;
using Musala.Drones.BuildingBlocks;

namespace Musala.Drones.Domain.Models;

public class Medication : BusinessEntity
{

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("weight")]
    public int Weight { get; set; }
    
    [JsonPropertyName("code")]
    public string Code { get; set; }
    
    [JsonPropertyName("image")]
    public string Image { get; set; }
    
    public virtual ICollection<MedicationCharge> MedicationCharges { get; set; }
}