using DronesTests.Fakes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace DronesTests;

public class RepositoryTests
{
    private IFakeRepository BuildRepository(string section)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<FakeDbContext>(cfg => 
                cfg.UseInMemoryDatabase($"repository{section}TestDatabase"))
            .AddLogging()
            .AddTransient<IFakeUnitOfWork, FakeUnitOfWork>()
            .AddTransient<IFakeRepository, FakeRepository>()
            .BuildServiceProvider();
        return serviceProvider.GetRequiredService<IFakeRepository>();
    }

    [Fact]
    public async void RepositoryTest_Create()
    {
        var repository = BuildRepository("Create");
        await repository.Create(new FakeDrone());
        await repository.Create(new FakeDrone());
        await repository.UnitOfWork.SaveChangesAsync();
        
        (await repository.Count()).ShouldBe(2);
        (await repository.Any()).ShouldBeTrue();
    }

    [Fact]
    public async void RepositoryTest_Query()
    {
        var repository = BuildRepository("Query");
        var code = "DRONE_CODE_001";
        await repository.Create(new FakeDrone { Code = code });
        await repository.UnitOfWork.SaveChangesAsync();
        
        var drone = await repository.Query.FirstOrDefaultAsync(x => x.Code.Equals(code));
        
        drone.ShouldNotBeNull();
        drone.Code.ShouldBe(code);
    }

    [Fact]
    public async void RepositoryTest_Update()
    {
        var repository = BuildRepository("Update");
        var id = 1;
        var code = "DRONE_CODE_001";
        var changed_code = "DRONE_CODE_002";
        var drone = new FakeDrone { Id = id, Code = code };
        await repository.Create(drone);
        await repository.UnitOfWork.SaveChangesAsync();
        drone = await repository.GetEntity<FakeDrone>().FirstOrDefaultAsync(x => x.Id == id);
        
        drone.ShouldNotBeNull();
        drone.Code.ShouldBe(code);

        drone.Code = changed_code;
        await repository.Update(id, drone);
        drone = await repository.GetEntity<FakeDrone>().FirstOrDefaultAsync(x => x.Id == id);      
        
        drone.ShouldNotBeNull();
        drone.Code.ShouldBe(changed_code);
    }

    [Fact]
    public async void RepositoryTest_Delete()
    {
        var repository = BuildRepository("Delete");
        await repository.Create(new FakeDrone { Id = 1 });
        await repository.Create(new FakeDrone ());
        await repository.Create(new FakeDrone ());
        await repository.UnitOfWork.SaveChangesAsync();
        
        (await repository.Count()).ShouldBe(3);
        await repository.Delete(1);
        await repository.UnitOfWork.SaveChangesAsync();
        
        (await repository.Count()).ShouldBe(2);
    }
}