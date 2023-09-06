namespace Musala.Drones.Contracts.Dtos.Responses;

public class ListMedicationsResponse : BaseResponse
{
    public ICollection<ListMedication> Medications { get; set; } = new HashSet<ListMedication>();
}

public class ListMedication
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public float Weight { get; set; }
    
    public string Code { get; set; }
    
    public string Image { get; set; }
}