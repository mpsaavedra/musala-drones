using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Musala.Drones.Api.Data;
using Musala.Drones.Api.Repositories;
using Musala.Drones.Api.Services;
using Musala.Drones.Contracts.Dtos.Requests;
using Musala.Drones.Contracts.Enums;
using Shouldly;

namespace DronesTests;

public class DispatcherTests
{
    private IDispatcherService BuildDispatcher(string section)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDbContext>(cfg => 
                cfg.UseInMemoryDatabase($"repository{section}TestDatabase"))
            .AddLogging()
            .AddAutoMapper(typeof(ApplicationDbContext))
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddTransient<IDroneRepository, DroneRepository>()
            .AddTransient<IDroneChargeRepository, DroneChargeRepository>()
            .AddTransient<IMedicationRepository, MedicationRepository>()
            .AddTransient<IMedicationChargeRepository, MedicationChargeRepository>()
            .AddTransient<IDispatcherService, DispatcherService>()
            .BuildServiceProvider();
        DbSeeder.SeedData(serviceProvider);
        return serviceProvider.GetRequiredService<IDispatcherService>();
    }

    [Fact]
    public void DispatcherTest_ListDrones()
    {
        var svr = BuildDispatcher("ListDrones");
        var response = svr.ListDrones();
        response.Drones.Count.ShouldBeGreaterThan(0);
        response.Drones.Count.ShouldBe(10);
    }

    [Fact]
    public void DispatcherTest_ListMedications()
    {
        var svr = BuildDispatcher("ListMedications");
        var response = svr.ListMedications();
        response.Medications.Count.ShouldBeGreaterThan(0);
        response.Medications.Count.ShouldBe(10);
    }

    [Fact]
    public void DispatcherTest_ListAvailableDronesForLoading()
    {
        var svr = BuildDispatcher("AvailableDronesForLoading");
        var response = svr.ListAvailableDronesForLoading();
        
        response.Success.ShouldBeTrue();
        response.AvailableDrones.Count.ShouldBeGreaterThan(0);
        response.AvailableDrones.Count.ShouldBe(6);
        response.AvailableDrones.Any(x => x.BatteryCapacity > 25 && 
                        x.State == DroneState.Idle).ShouldBeTrue();
    }

    [Fact]
    public void DispatcherTest_BatteryLevelOnDrone_Ok()
    {
        var svr = BuildDispatcher("BatterLevelOnDrone_Ok");
        var request = new BatteryLevelInDroneRequest { SerialNumber = "DRONE_001" };
        var response = svr.BatteryLevelOnDrone(request);
        response.Success.ShouldBeTrue();
        response.BatteryLevel.ShouldBe(100);
    }

    [Fact]
    public void DispatcherTest_BatteryLevelOnDrone_Fail()
    {
        var svr = BuildDispatcher("BatterLevelOnDrone_Fail");
        var request = new BatteryLevelInDroneRequest { SerialNumber = "DRONE_012" };
        var response = svr.BatteryLevelOnDrone(request);
        response.Success.ShouldBeFalse();
        response.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void DispatcherTest_MedicationsInDrone_Ok()
    {
        var svr = BuildDispatcher("MedicationsInDrone_Ok");
        var request = new MedicationsInDroneRequest { SerialNumber = "DRONE_001" };
        var response = svr.MedicationsInDrone(request);
        response.Success.ShouldBeTrue();
        response.Medications.Count.ShouldBeGreaterThan(0);
        response.Medications.Count.ShouldBe(5);
    }

    [Fact]
    public void DispatcherTest_MedicationsInDrone_Fail_SerialNumber()
    {
        var svr = BuildDispatcher("MedicationsInDrone_Fail_SerialNumber");
        var request = new MedicationsInDroneRequest { SerialNumber = "DRONE_012" };
        var response = svr.MedicationsInDrone(request);
        response.Success.ShouldBeFalse();
        response.Message.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void DispatcherTest_MedicationsInDrone_Fail_State()
    {
        var svr = BuildDispatcher("MedicationsInDrone_Fail_SerialNumber");
        var request = new MedicationsInDroneRequest { SerialNumber = "DRONE_003" };
        var response = svr.MedicationsInDrone(request);
        response.Success.ShouldBeFalse();
        response.Message.ShouldNotBeNullOrEmpty();
    }
}