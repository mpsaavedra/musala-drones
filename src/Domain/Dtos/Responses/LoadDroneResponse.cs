using Musala.Drones.Domain.Dtos.Requests;

namespace Musala.Drones.Domain.Dtos.Responses;

public class LoadDroneResponse : BaseResponse
{
    public string SerialNumber { get; set; }

    public List<MedicationLoad> Medications { get; set; } = new List<MedicationLoad>();
}