using Musala.Drones.Api.Data;
using Musala.Drones.BuildingBlocks;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Repositories;

public interface IMedicationChargeRepository : IRepository<MedicationCharge, ApplicationDbContext>
{
}

public class MedicationChargeRepository : Repository<MedicationCharge, ApplicationDbContext>, IMedicationChargeRepository
{
    public MedicationChargeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}