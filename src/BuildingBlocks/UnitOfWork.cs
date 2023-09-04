using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Musala.Drones.BuildingBlocks;

public interface IUnitOfWork<TContext>
    where TContext : DbContext
{
    IDbContextTransaction? Transaction { get; }

    TContext Context { get; }

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task CloseTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);

    T? Repository<T>() where T : class;

    T? Service<T>() where T : class;
}


public class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private readonly IServiceProvider _provider;
    
    public UnitOfWork(IServiceProvider? provider, TContext? context)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public IDbContextTransaction? Transaction { get; private set; }
    
    public TContext Context { get; }
    
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        // do not use transactions hen using InMemory provider
        if (Context.Database.ProviderName != null && Context.Database.ProviderName.Contains("InMemory"))
            return;

        if (Transaction != null)
        {
            await Transaction.DisposeAsync();
        }
        
        Transaction = await Context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = 0;
        try
        {
            result = await Context.SaveChangesAsync(cancellationToken);

            if (Transaction != null)
            {
                await Task.Run(() => Transaction!.Commit(), cancellationToken);
            }
        }
        catch (Exception exception)
        {
            await CloseTransactionAsync();
            throw;
        }

        return result;
    }

    public async Task CloseTransactionAsync(CancellationToken cancellationToken = default) =>
        Transaction?.DisposeAsync();

    public async Task RollbackAsync(CancellationToken cancellationToken = default) =>
        await Transaction?.RollbackAsync(cancellationToken)!;

    public T? Repository<T>() where T : class =>
        _provider.GetRequiredService(typeof(T)) as T;

    public T? Service<T>() where T : class =>
        _provider.GetRequiredService(typeof(T)) as T;
}