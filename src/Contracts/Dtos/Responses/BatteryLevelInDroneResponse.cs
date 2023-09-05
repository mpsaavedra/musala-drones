namespace Musala.Drones.Contracts.Dtos.Responses;

public class BatteryLevelInDroneResponse : BaseResponse
{
    public string SerialNumber { get; set; }
    
    public float BatteryLevel { get; set; }
}