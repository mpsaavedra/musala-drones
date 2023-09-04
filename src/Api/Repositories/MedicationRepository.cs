using Musala.Drones.Api.Data;
using Musala.Drones.BuildingBlocks;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Repositories;

public interface IMedicationRepository : IRepository<Medication, ApplicationDbContext>
{
}

public class MedicationRepository : Repository<Medication, ApplicationDbContext>, IMedicationRepository
{
    public MedicationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}