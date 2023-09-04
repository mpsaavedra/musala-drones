using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musala.Drones.BuildingBlocks.Extensions;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Domain.EntityConfigurations;

public class MedicationEntityTypeConfiguration : 
    IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.ConfigureBusinessEntity<Medication>();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Code);
        builder.HasMany(x => x.MedicationCharges)
            .WithOne(x => x.Medication)
            .HasForeignKey(x => x.MedicationId);
    }
}