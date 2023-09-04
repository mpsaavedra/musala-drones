using Musala.Drones.Api.Data;
using Musala.Drones.BuildingBlocks;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Repositories;

public interface IDroneRepository : IRepository<Drone, ApplicationDbContext>
{
    
}
public class DroneRepository : Repository<Drone, ApplicationDbContext>, IDroneRepository
{
    public DroneRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}