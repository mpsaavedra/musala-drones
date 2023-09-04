using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musala.Drones.BuildingBlocks.Extensions;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Domain.EntityConfigurations;

public class DroneEntityTypeConfiguration :
    IEntityTypeConfiguration<Drone>
{
    public void Configure(EntityTypeBuilder<Drone> builder)
    {
        builder.ConfigureBusinessEntity<Drone>();
        builder.Property(x => x.SerialNumber).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Model).IsRequired();
        builder.Property(x => x.WeightLimit).IsRequired();
        builder.Property(x => x.BatteryCapacity).IsRequired();
        builder.Property(x => x.State).IsRequired();
        builder.HasIndex(x => x.SerialNumber).IsUnique();
    }
}