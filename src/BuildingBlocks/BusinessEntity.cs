using System.Text.Json.Serialization;

namespace Musala.Drones.BuildingBlocks;

/// <summary>
/// base entity
/// </summary>
public interface IBusinessEntity
{
    int Id { get; set; }
    
    DateTime CreatedAt { get; set; }
    
    string RowVersion { get; set; }
}

/// <inheritdoc cref="IBusinessEntity"/>
public class BusinessEntity : IBusinessEntity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonIgnore]
    [JsonPropertyName("createAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    [JsonPropertyName("rowVersion")]
    public string RowVersion { get; set; } = Guid.NewGuid().ToString();
}