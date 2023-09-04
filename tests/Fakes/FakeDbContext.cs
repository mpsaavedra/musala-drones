using Microsoft.EntityFrameworkCore;

namespace DronesTests.Fakes;

public class FakeDbContext : DbContext
{
    public FakeDbContext(DbContextOptions<FakeDbContext> options) : base(options)
    {
    }

    public virtual DbSet<FakeDrone> Drones => Set<FakeDrone>();
}