using Musala.Drones.BuildingBlocks;

namespace Musala.Drones.Domain.Models;

public class MedicationCharge : BusinessEntity
{
    public int MedicationId { get; set; }
    public virtual Medication Medication { get; set; }
    public int DroneChargeId { get; set; }
    public virtual DroneCharge DroneCharge { get; set; }
}