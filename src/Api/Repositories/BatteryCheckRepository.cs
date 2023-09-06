using Musala.Drones.Api.Data;
using Musala.Drones.BuildingBlocks;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Repositories;

public interface IBatteryAuditRepository : IRepository<BatteryAudit, ApplicationDbContext>
{
}

public class BatteryAuditRepository : Repository<BatteryAudit, ApplicationDbContext>, IBatteryAuditRepository
{
    public BatteryAuditRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}