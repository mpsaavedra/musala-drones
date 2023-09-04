using Musala.Drones.BuildingBlocks;

namespace Musala.Drones.Domain.Models;

public class DroneCharge : BusinessEntity
{
    public int DroneId { get; set; }
    
    public virtual Drone Drone { get; set; }
    
    public virtual ICollection<MedicationCharge> MedicationCharges { get; set; } = new HashSet<MedicationCharge>();
}