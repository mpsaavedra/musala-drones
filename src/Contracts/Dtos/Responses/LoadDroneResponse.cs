namespace Musala.Drones.Contracts.Dtos.Responses;

public class LoadDroneResponse : BaseResponse
{
    public int Id { get; set; }

    public List<MedicationLoaded> Medications { get; set; } = new List<MedicationLoaded>();
}

public class MedicationLoaded
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public float Weight { get; set; }
    
    public string Code { get; set; }
    
    public string Image { get; set; }
}