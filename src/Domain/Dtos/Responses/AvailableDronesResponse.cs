namespace Musala.Drones.Domain.Dtos.Responses;

public class AvailableDronesResponse : BaseResponse
{
    public ICollection<AvailableDrones> AvailableDrones { get; set; } = new HashSet<AvailableDrones>();
}

public class AvailableDrones
{
    public string SerialNumber { get; set; }
    
    public float BatteryLevel { get; set; }
}