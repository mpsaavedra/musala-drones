namespace Musala.Drones.Domain.Dtos.Responses;

public class ListMedicationsResponse : BaseResponse
{
    public ICollection<ListMedication> Medications { get; set; } = new HashSet<ListMedication>();
}

public class ListMedication
{
    public string Name { get; set; }
    
    public float Weight { get; set; }
    
    public string Code { get; set; }
    
    public string Image { get; set; }
}