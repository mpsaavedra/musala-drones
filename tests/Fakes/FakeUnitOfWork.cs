using Musala.Drones.BuildingBlocks;

namespace DronesTests.Fakes;

public interface IFakeUnitOfWork : IUnitOfWork<FakeDbContext>
{
}

public class FakeUnitOfWork : UnitOfWork<FakeDbContext>, IFakeUnitOfWork
{
    public FakeUnitOfWork(IServiceProvider? provider, FakeDbContext? context) : base(provider, context)
    {
    }
}