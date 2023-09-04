namespace Musala.Drones.Domain.Dtos.Responses;

public class ListDronesResponse : BaseResponse
{
    public ICollection<ListDrone> ListDrones { get; set; } = new HashSet<ListDrone>();
}

public class ListDrone
{
    public string SerialNumber { get; set; }
    
    public float BatteryLevel { get; set; }
    
    public float WeightCapacity { get; set; }
    
    public bool AvailableForLoading { get; set; }
}