using Musala.Drones.BuildingBlocks;

namespace Musala.Drones.Domain.Models;

public class DroneCharge : BusinessEntity
{
    public int DroneId { get; set; }
    public virtual Drone Drone { get; set; }
    public virtual ICollection<Medication> Medications { get; set; } = new HashSet<Medication>();
}