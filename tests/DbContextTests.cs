using DronesTests.Fakes;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DronesTests;

public class DbContextTests
{
    private FakeDbContext BuildContext(string section)
    {
        var options = new DbContextOptionsBuilder<FakeDbContext>()
            .UseInMemoryDatabase($"dbContext{section}TestDatabase")
            .Options;
        return new FakeDbContext(options);
    }
    
    [Fact]
    public void DbContextTest_Add()
    {
        var context = BuildContext("Add");
        context.Drones.Add(new FakeDrone());
        context.SaveChanges();
        
        context.Drones.Count().ShouldBe(1);

        context.Drones.Add(new FakeDrone());
        context.Drones.Add(new FakeDrone());
        context.SaveChanges();

        context.Drones.Count().ShouldBe(3);
    }

    [Fact]
    public void DbContextTest_Update()
    {
        const string code = "DRONE_CODE_001";
        const string changed_code = "DRONE_CODE_002";
        var context = BuildContext("Update");

        context.Drones.Add(new FakeDrone { Code = code });
        context.SaveChanges();

        var drone = context.Drones.FirstOrDefault();
        drone.ShouldNotBeNull();
        drone.Code.ShouldBe(code);

        drone.Code = changed_code;
        context.Update(drone);
        context.SaveChanges();
        
        context.Drones.First().Code.ShouldBe(changed_code);
    }

    [Fact]
    public void DbContextTest_Remove()
    {
        const string code = "DRONE_CODE_001";
        var context = BuildContext("Remove");
        context.Drones.Add(new FakeDrone { Code = code });
        context.Drones.Add(new FakeDrone());
        context.Drones.Add(new FakeDrone());
        context.SaveChanges();

        context.Drones.Count().ShouldBe(3);

        var drone = context.Drones.FirstOrDefault(x => x.Code.Equals(code));
        context.Drones.Remove(drone);
        context.SaveChanges();
        
        context.Drones.Count().ShouldBe(2);
    }
}