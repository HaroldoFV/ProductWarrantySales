using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Application.Exceptions;
using ProductMicroservice.Domain.Entity;
using ProductMicroservice.Domain.Repository;
using ProductMicroservice.Domain.SeedWork.SearchableRepository;

namespace ProductMicroservice.Infra.Data.EF.Repositories;

public class ProductRepository
    : IProductRepository
{
    private readonly ProductWarrantySalesDbContext _context;
    private DbSet<Product> _products => _context.Set<Product>();

    public ProductRepository(ProductWarrantySalesDbContext context) => _context = context;


    public async Task InsertAsync(Product aggregate, CancellationToken cancellationToken)
    {
        await _products.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _products.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(product, $"Product '{id}' not found.");

        return product!;
    }

    public Task DeleteAsync(Product aggregate, CancellationToken _)
    {
        return Task.FromResult(_products.Remove(aggregate));
    }

    public Task UpdateAsync(Product aggregate, CancellationToken _)
    {
        return Task.FromResult(_products.Update(aggregate));
    }

    public async Task<SearchOutput<Product>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _products.AsNoTracking();
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

    private IQueryable<Product> AddOrderToQuery(IQueryable<Product> query, string inputOrderBy, SearchOrder inputOrder)
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