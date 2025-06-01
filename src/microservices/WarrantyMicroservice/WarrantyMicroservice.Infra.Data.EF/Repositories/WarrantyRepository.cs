using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Application.Exceptions;
using WarrantyMicroservice.Domain.Entity;
using WarrantyMicroservice.Domain.Repository;
using WarrantyMicroservice.Domain.SeedWork.SearchableRepository;

namespace WarrantyMicroservice.Infra.Data.EF.Repositories;

public class WarrantyRepository
    : IWarrantyRepository
{
    private readonly WarrantyDbContext _context;
    private DbSet<Warranty> _warranties => _context.Set<Warranty>();

    public WarrantyRepository(WarrantyDbContext context) => _context = context;


    public async Task InsertAsync(Warranty aggregate, CancellationToken cancellationToken)
    {
        await _warranties.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Warranty> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _warranties.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(product, $"Warranty '{id}' not found.");

        return product!;
    }

    public Task DeleteAsync(Warranty aggregate, CancellationToken _)
    {
        return Task.FromResult(_warranties.Remove(aggregate));
    }

    public Task UpdateAsync(Warranty aggregate, CancellationToken _)
    {
        return Task.FromResult(_warranties.Update(aggregate));
    }

    public async Task<SearchOutput<Warranty>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _warranties.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if (!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));
        var total = await query.CountAsync(cancellationToken: cancellationToken);
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync(cancellationToken: cancellationToken);

        return new(input.Page, input.PerPage, total, items);
    }

    private IQueryable<Warranty> AddOrderToQuery(IQueryable<Warranty> query, string inputOrderBy,
        SearchOrder inputOrder)
    {
        var orderedQuery = (inputOrderBy.ToLower(), inputOrder) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}