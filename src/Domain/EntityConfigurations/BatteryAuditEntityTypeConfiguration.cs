using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musala.Drones.BuildingBlocks.Extensions;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Domain.EntityConfigurations;

public class BatteryAuditEntityTypeConfiguration : IEntityTypeConfiguration<BatteryAudit>
{
    public void Configure(EntityTypeBuilder<BatteryAudit> builder)
    {
        builder.ConfigureBusinessEntity();
        builder.Property(x => x.BatteryCapacity).HasMaxLength(100).IsRequired();
        builder.HasOne<Drone>(x => x.Drone)
            .WithMany(x => x.BatteryAudits)
            .HasForeignKey(x => x.DroneId);
    }
}