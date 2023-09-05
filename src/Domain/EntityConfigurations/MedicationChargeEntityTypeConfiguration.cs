using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musala.Drones.BuildingBlocks.Extensions;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Domain.EntityConfigurations;

public class MedicationChargeEntityTypeConfiguration : IEntityTypeConfiguration<MedicationCharge>
{
    public void Configure(EntityTypeBuilder<MedicationCharge> builder)
    {
        builder.ConfigureBusinessEntity();
        builder.HasOne(x => x.Medication)
            .WithMany(x => x.MedicationCharges)
            .HasForeignKey(x => x.MedicationId);
        builder.HasOne(x => x.DroneCharge)
            .WithMany(x => x.MedicationCharges)
            .HasForeignKey(x => x.DroneChargeId);
    }
}