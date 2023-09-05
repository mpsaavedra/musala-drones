using Musala.Drones.Domain.Enums;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Data;

public static class DbSeeder
{
    public static void SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Drones.Any()) return;

        #region Drone register

        var drone1 = context.Drones.Add(new Drone
        {
            Id = 1, Model = DroneModel.HeavyWeight, State = DroneState.Delivering,
            SerialNumber = "DRONE_001", BatteryCapacity = 100, WeightLimit = 500
        }).Entity;

        var drone2 = context.Drones.Add(new Drone
        {
            Id = 2, Model = DroneModel.CruiserWeight, State = DroneState.Loading,
            SerialNumber = "DRONE_002", BatteryCapacity = 100, WeightLimit = 400
        }).Entity;

        var drone3 = context.Drones.Add(new Drone
        {
            Id = 3, Model = DroneModel.LightWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_003", BatteryCapacity = 100, WeightLimit = 100
        }).Entity;

        var drone4 = context.Drones.Add(new Drone
        {
            Id = 4, Model = DroneModel.HeavyWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_004", BatteryCapacity = 100, WeightLimit = 400
        }).Entity;

        var drone5 = context.Drones.Add(new Drone
        {
            Id = 5, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_005", BatteryCapacity = 25, WeightLimit = 400
        }).Entity;

        var drone6 = context.Drones.Add(new Drone
        {
            Id = 6, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_006", BatteryCapacity = 80, WeightLimit = 400
        }).Entity;

        var drone7 = context.Drones.Add(new Drone
        {
            Id = 7, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_007", BatteryCapacity = 95, WeightLimit = 400
        }).Entity;

        var drone8 = context.Drones.Add(new Drone
        {
            Id = 8, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_008", BatteryCapacity = 15, WeightLimit = 400
        }).Entity;

        var drone9 = context.Drones.Add(new Drone
        {
            Id = 9, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_009", BatteryCapacity = 100, WeightLimit = 400
        }).Entity;

        var drone10 = context.Drones.Add(new Drone
        {
            Id = 10, Model = DroneModel.CruiserWeight, State = DroneState.Idle,
            SerialNumber = "DRONE_010", BatteryCapacity = 100, WeightLimit = 400
        }).Entity;

        context.SaveChanges();

        #endregion

        #region Medicine register

        var medicine1 = context.Medications.Add(new Medication
        {
            Id = 1, Code = "001_ADVIL", Image = "001-advil.png", Name = "Advil-200mg_Bottle",
            Weight = 20
        }).Entity;

        var medicine2 = context.Medications.Add(new Medication
        {
            Id = 2, Code = "001_ALEVE", Image = "001-aleve.png", Name = "Aleve_Bottle",
            Weight = 50
        }).Entity;

        var medicine3 = context.Medications.Add(new Medication
        {
            Id = 3, Code = "001_ALEVE", Image = "001-aspirine.png", Name = "Aspirine-81mg_Bottle",
            Weight = 40
        }).Entity;

        var medicine4 = context.Medications.Add(new Medication
        {
            Id = 4, Code = "001_ATRIPLA", Image = "001-atripla.png", Name = "Atripla-30-tablets_Bottle",
            Weight = 50
        }).Entity;

        var medicine5 = context.Medications.Add(new Medication
        {
            Id = 5, Code = "001_KALCIPOS", Image = "001-calcium.png", Name = "Kalcipos-D_Mite_600mg-Bottle",
            Weight = 100
        }).Entity;

        var medicine6 = context.Medications.Add(new Medication
        {
            Id = 6, Code = "001_GABAPETIN", Image = "001-gabapetin.png", Name = "Gabapetin-600mg-Tablets_Bottle",
            Weight = 200
        }).Entity;

        var medicine7 = context.Medications.Add(new Medication
        {
            Id = 7, Code = "001_GLOCOSAMINE", Image = "001-glucosamine.png", Name = "Glucosamine-60_Tablets_Bottle",
            Weight = 400
        }).Entity;

        var medicine8 = context.Medications.Add(new Medication
        {
            Id = 8, Code = "001_MICIMEX", Image = "001-mucimex.png", Name = "Mucinex-1200mg-Tablets_Bottle",
            Weight = 200
        }).Entity;

        var medicine9 = context.Medications.Add(new Medication
        {
            Id = 9, Code = "001_PARACETAMOL", Image = "001-paracetamol.png",
            Name = "Paracetamol-500mg-Tablets_Bottle",
            Weight = 500
        }).Entity;

        var medicine10 = context.Medications.Add(new Medication
        {
            Id = 10, Code = "001_RIMADYL", Image = "001-rimadyl.png",
            Name = "Rimadyl-75mg-chewables_Bottle",
            Weight = 100
        }).Entity;

        context.SaveChanges();

        #endregion

        #region Drone charge register

        var droneCharge1 = context.DroneCharges.Add(new DroneCharge { Drone = drone1 }).Entity;

        var droneCharge2 = context.DroneCharges.Add(new DroneCharge { Drone = drone2 }).Entity;

        context.SaveChanges();

        #endregion

        #region Medication Charge register

        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge1, Medication = medicine1 });
        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge1, Medication = medicine2 });
        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge1, Medication = medicine3 });
        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge1, Medication = medicine4 });
        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge1, Medication = medicine5 });
        
        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge2, Medication = medicine3 });
        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge2, Medication = medicine4 });
        context.MedicationCharges.Add(new MedicationCharge { DroneCharge = droneCharge2, Medication = medicine10 });

        context.SaveChanges();

        #endregion
    }
}