namespace Musala.Drones.Domain.Dtos.Requests;

public class LoadDroneRequest
{
    public string SerialNumber { get; set; }

    public List<MedicationLoad> Medications { get; set; } = new List<MedicationLoad>();
}

public class MedicationLoad
{
    public string Code { get; set; }
}