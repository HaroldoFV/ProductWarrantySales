using Microsoft.Extensions.Logging;
using WarrantyMicroservice.Application.Interfaces;
using WarrantyMicroservice.Domain.SeedWork;

namespace WarrantyMicroservice.Infra.Data.EF;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly WarrantyDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(
        WarrantyDbContext context,
        ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        var aggregateRoots = _context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity);

        var enumerable = aggregateRoots as AggregateRoot[] ?? aggregateRoots.ToArray();
        _logger.LogInformation(
            "Commit: {AggregatesCount} aggregate roots with events.",
            enumerable.Count());

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}