namespace Musala.Drones.Contracts.Dtos.Responses;

public class MedicationsInDroneResponse : BaseResponse
{
    public ICollection<MedicationInDrone> Medications { get; set; } = new HashSet<MedicationInDrone>();
}

public class MedicationInDrone
{
    public string Name { get; set; }
    
    public float Weight { get; set; }
    
    public string Code { get; set; }
    
    public string Image { get; set; }
}