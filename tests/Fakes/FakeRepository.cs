using Musala.Drones.BuildingBlocks;

namespace DronesTests.Fakes;

public interface IFakeRepository : IRepository<FakeDrone, FakeDbContext>
{
}

public class FakeRepository : Repository<FakeDrone, FakeDbContext>, IFakeRepository
{
    public FakeRepository(IFakeUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}