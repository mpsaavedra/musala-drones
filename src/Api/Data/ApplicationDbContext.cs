using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Drone> Drones => Set<Drone>();

    public virtual DbSet<DroneCharge> DroneCharges => Set<DroneCharge>();

    public virtual DbSet<Medication> Medications => Set<Medication>();

    public virtual DbSet<MedicationCharge> MedicationCharges => Set<MedicationCharge>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseInMemoryDatabase("MusalaDrones");

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder
    //         .EnableSensitiveDataLogging()
    //         .UseInMemoryDatabase("MusalaDrones");
    // }
}

// public class DesignTimeApplicationDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
// {
//     public ApplicationDbContext CreateDbContext(string[] args)
//     {
//         var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//             .UseInMemoryDatabase("MusalaDrones")
//             .Options;
//         return new ApplicationDbContext(options);
//     }
// }