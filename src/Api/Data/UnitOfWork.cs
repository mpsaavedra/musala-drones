using Musala.Drones.BuildingBlocks;

namespace Musala.Drones.Api.Data;

public interface IUnitOfWork : IUnitOfWork<ApplicationDbContext>
{
    
}

public class UnitOfWork : UnitOfWork<ApplicationDbContext>, IUnitOfWork
{
    public UnitOfWork(IServiceProvider? provider, ApplicationDbContext? context) : base(provider, context)
    {
    }
}