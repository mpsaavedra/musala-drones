namespace Musala.Drones.Contracts.Dtos.Requests;

public class LoadDroneRequest
{
    public int Id { get; set; }

    public ICollection<int> MedicationIds { get; set; } = new HashSet<int>();
}