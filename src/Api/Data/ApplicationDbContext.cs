using Microsoft.EntityFrameworkCore;
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
}