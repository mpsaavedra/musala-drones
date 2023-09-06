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
        return await UnitOfWork.ExecuteAsync<int>(async () =>
        {
            EntityEntry<TEntity> entityEntry = null;
            if (Context.Database.ProviderName != null && Context.Database.ProviderName.Contains("InMemory"))
            {
                entityEntry = await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
                await Context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(entityEntry.Entity.Id);
            }

            using (var transaction = await Context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    entityEntry = await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
                    await Context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return await Task.FromResult(entityEntry.Entity.Id);
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }, cancellationToken);
    }

    public async Task<bool> Update(int id, TEntity entity, CancellationToken cancellationToken = default)
    {
        return await UnitOfWork.ExecuteAsync<bool>(async () =>
        {
            if (Context.Database.ProviderName != null && Context.Database.ProviderName.Contains("InMemory"))
            {
                Context.Set<TEntity>().Update(entity);
                Context.Entry<TEntity>(entity).Property("CreatedAt").IsModified = false;
                var result = await Context.SaveChangesAsync(cancellationToken) > 0;
                return result;
            }
            
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.Set<TEntity>().Update(entity);
                    Context.Entry<TEntity>(entity).Property("CreatedAt").IsModified = false;
                    var result = await Context.SaveChangesAsync(cancellationToken) > 0;
                    await transaction.CommitAsync(cancellationToken);
                    return result;
                }
                catch 
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return false;
                }
            }
        }, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        return await UnitOfWork.ExecuteAsync(async () =>
        {
            if (Context.Database.ProviderName != null && Context.Database.ProviderName.Contains("InMemory"))
            {
                var entity = await Context.Set<TEntity>()
                    .FirstOrDefaultAsync(x => x.Id!.Equals(id), cancellationToken);
                if (entity == null) return false;
                Context.Set<TEntity>().Remove(entity);
                var result = await Context.SaveChangesAsync(cancellationToken) > 0;
                return result;
            }
            
            using (var transaction = await Context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var entity = await Context.Set<TEntity>()
                        .FirstOrDefaultAsync(x => x.Id!.Equals(id), cancellationToken);
                    if (entity == null) return false;
                    Context.Set<TEntity>().Remove(entity);
                    var result = await Context.SaveChangesAsync(cancellationToken) > 0;
                    await transaction.CommitAsync(cancellationToken);
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return false;
                }
            }
        }, cancellationToken);
    }

    public Task<int> Count() => Query.CountAsync();

    public Task<bool> Any() => Query.AnyAsync();
}