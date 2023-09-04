using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Musala.Drones.BuildingBlocks.Extensions;

public static class BusinessEntityExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureBusinessEntity<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        bool generatedKeyValue = true)
        where TEntity : class, IBusinessEntity
    {
        if (generatedKeyValue)
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        else
            builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.RowVersion).IsRequired();
        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.RowVersion);
        return builder;
    }
}