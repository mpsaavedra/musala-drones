using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Musala.Drones.BuildingBlocks;

public interface IUnitOfWork<TContext>
    where TContext : DbContext
{

    TContext Context { get; }

    Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken = default);

    Task ExecuteAsync(Action operation, Func<bool>? verifySucceeded = null,
        CancellationToken cancellationToken = default);
    

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
    
    public TContext Context { get; }
    
    public async  Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken = default)
    {
        return await Task.Run(async () =>
        {
            var executionStrategy = Context.Database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(operation);
        }, cancellationToken);
    }

    public async Task ExecuteAsync(Action operation, Func<bool>? verifySucceeded = null,
        CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            verifySucceeded ??= (() => false);
            var executionStrategy = Context.Database.CreateExecutionStrategy();
            executionStrategy.ExecuteInTransaction(operation, verifySucceeded);
        }, cancellationToken);
    }

    public T? Repository<T>() where T : class =>
        _provider.GetRequiredService(typeof(T)) as T;

    public T? Service<T>() where T : class =>
        _provider.GetRequiredService(typeof(T)) as T;
}