#nullable  enable
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Musala.Drones.BuildingBlocks.Extensions;

namespace Musala.Drones.BuildingBlocks;


public interface IRepository<TEntity, TContext> 
    where TEntity : class, IBusinessEntity
    where TContext : DbContext
{
    TContext Context { get; }
    
    IUnitOfWork<TContext> UnitOfWork { get; }
    
    DbSet<T> GetEntity<T>() where T : class;
    
    IQueryable<TEntity> Query => Context.Set<TEntity>();
    
    Task<int> Create(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<bool> Update(int id, TEntity entity, CancellationToken cancellationToken = default);
    
    Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    
    Task<int> Count();
    
    Task<bool> Any();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

public class Repository<TEntity, TContext> :
    IRepository<TEntity, TContext>
    where TEntity : class, IBusinessEntity
    where TContext : DbContext
{
    public TContext Context { get; }

    public IUnitOfWork<TContext> UnitOfWork { get; }

    public IQueryable<TEntity> Query => Context.Set<TEntity>();

    public DbSet<T> GetEntity<T>() where T : class => Context.Set<T>();

    public Repository(IUnitOfWork<TContext> unitOfWork)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Context = unitOfWork.Context ?? throw new ArgumentNullException(nameof(unitOfWork.Context));
    }

    public async Task<int> Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<TEntity> entityEntry;
        if (UnitOfWork is null)
        {
            entityEntry = await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }
        else
        {
            await UnitOfWork.BeginTransactionAsync(cancellationToken);
            entityEntry = await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
        }

        return entityEntry.Entity.Id;
    }

    public async Task<bool> Update(int id, TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (UnitOfWork is null)
        {
            Context.Set<TEntity>().Update(entity);
            Context.Entry<TEntity>(entity).Property("CreatedAt").IsModified = false;
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        await UnitOfWork.BeginTransactionAsync(cancellationToken);
        Context.Set<TEntity>().Update(entity);
        Context.Entry<TEntity>(entity).Property("CreatedAt").IsModified = false;
        return await UnitOfWork.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id!.Equals(id), cancellationToken);
        if (entity == null)
            return false;

        if (UnitOfWork is null)
        {
            Context.Set<TEntity>().Remove(entity);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        await UnitOfWork.BeginTransactionAsync(cancellationToken);
        Context.Set<TEntity>().Remove(entity);
        return await UnitOfWork.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }

    public Task<int> Count() => Query.CountAsync();

    public Task<bool> Any() => Query.AnyAsync();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // TODO: implement the business entity data update
        return await Context.SaveChangesAsync(cancellationToken);
    }
}