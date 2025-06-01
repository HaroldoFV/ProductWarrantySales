using MediatR;
using Microsoft.EntityFrameworkCore;
using SaleMicroservice.API.Common.Exceptions;
using SaleMicroservice.API.Common.Infrastructure.Persistence;

namespace SaleMicroservice.API.Endpoints.Sales.Get;

public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResponse>
{
    private readonly SalesDbContext _context;

    public GetSaleHandler(SalesDbContext context)
    {
        _context = context;
    }

    public async Task<GetSaleResponse> Handle(
        GetSaleQuery request,
        CancellationToken cancellationToken)
    {
        var sale = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (sale == null)
            throw new NotFoundException("Sale not found");

        return new GetSaleResponse(
            sale.Id,
            sale.TotalAmount,
            sale.Items.Select(item => new SaleItemResponse(
                item.ProductId,
                item.Quantity,
                item.UnitPrice,
                item.TotalAmount,
                item.Warranty != null && item.Warranty.Value > 0
            )).ToList()
        );
    }
}