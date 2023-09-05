using Musala.Drones.Domain.Dtos.Requests;

namespace Musala.Drones.Domain.Dtos.Responses;

public class LoadDroneResponse : BaseResponse
{
    public string SerialNumber { get; set; }

    public List<MedicationLoaded> Medications { get; set; } = new List<MedicationLoaded>();
}

public class MedicationLoaded
{
    public string Name { get; set; }
    
    public float Weight { get; set; }
    
    public string Code { get; set; }
    
    public string Image { get; set; }
}