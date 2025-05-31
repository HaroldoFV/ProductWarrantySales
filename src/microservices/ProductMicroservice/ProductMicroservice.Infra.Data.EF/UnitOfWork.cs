using Microsoft.Extensions.Logging;
using ProductMicroservice.Application.Interfaces;
using ProductMicroservice.Domain.SeedWork;

namespace ProductMicroservice.Infra.Data.EF;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly ProductWarrantySalesDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(
        ProductWarrantySalesDbContext context,
        ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Commit(CancellationToken cancellationToken)
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

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}