using MediatR;
using SaleMicroservice.API.Common.Exceptions;
using SaleMicroservice.API.Common.Infrastructure.Persistence;

namespace SaleMicroservice.API.Endpoints.Sales.Delete;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand>
{
    private readonly SalesDbContext _context;

    public DeleteSaleHandler(SalesDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales.FindAsync(
            new object[] { request.Id },
            cancellationToken);

        if (sale == null)
            throw new NotFoundException("Sale not found");

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }
}