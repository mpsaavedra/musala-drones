using Musala.Drones.Domain.Enums;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Data;

public static class DbSeeder
{
    public static void SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (!context.Drones.Any())
        {
            #region Drone register

            var drone1 = context.Drones.Add(new Drone
            {
                Id = 1, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
                SerialNumber = "B98FD5D3-7929-45D6-9942-CB126C125FFB", BatteryCapacity = 100
            }).Entity;
            
            var drone2 = context.Drones.Add(new Drone
            {
                Id = 1, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
                SerialNumber = "B98FD5D3-7929-45D6-9942-CB126C125FFB", BatteryCapacity = 100
            }).Entity;
            
            var drone3 = context.Drones.Add(new Drone
            {
                Id = 1, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
                SerialNumber = "B98FD5D3-7929-45D6-9942-CB126C125FFB", BatteryCapacity = 100
            }).Entity;
            
            var drone4 = context.Drones.Add(new Drone
            {
                Id = 1, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
                SerialNumber = "B98FD5D3-7929-45D6-9942-CB126C125FFB", BatteryCapacity = 100
            }).Entity;
            
            var drone5 = context.Drones.Add(new Drone
            {
                Id = 1, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
                SerialNumber = "B98FD5D3-7929-45D6-9942-CB126C125FFB", BatteryCapacity = 100
            }).Entity;

            context.SaveChanges();
            
            #endregion
        }
    }
}