using Musala.Drones.Api.Data;
using Musala.Drones.BuildingBlocks;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Repositories;

public interface IDroneChargeRepository : IRepository<DroneCharge, ApplicationDbContext>
{
    
}

public class DroneChargeRepository : Repository<DroneCharge, ApplicationDbContext>, IDroneChargeRepository
{
    public DroneChargeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}