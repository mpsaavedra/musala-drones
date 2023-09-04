using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musala.Drones.BuildingBlocks.Extensions;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Domain.EntityConfigurations;

public class DroneChargeEntityTypeConfiguration : IEntityTypeConfiguration<DroneCharge>
{
    public void Configure(EntityTypeBuilder<DroneCharge> builder)
    {
        builder.ConfigureBusinessEntity();
        builder.HasOne<Drone>(x => x.Drone)
            .WithMany(x => x.DroneCharges)
            .HasForeignKey(x => x.DroneId);
        builder.HasMany<MedicationCharge>(x => x.MedicationCharges)
            .WithOne(x => x.DroneCharge)
            .HasForeignKey(x => x.DroneChargeId);
    }
}