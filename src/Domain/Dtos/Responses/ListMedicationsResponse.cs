namespace Musala.Drones.Domain.Dtos.Responses;

public class ListMedicationsResponse : BaseResponse
{
    public string Name { get; set; }
    
    public float Weight { get; set; }
    
    public string Code { get; set; }
    
    public string Image { get; set; }
}