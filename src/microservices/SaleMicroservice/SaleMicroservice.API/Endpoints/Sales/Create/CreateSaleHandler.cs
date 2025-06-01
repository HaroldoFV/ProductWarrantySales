using FluentValidation;
using MediatR;
using SaleMicroservice.API.Common.Infrastructure.Persistence;
using SaleMicroservice.API.Endpoints.Sales.Domain;
using SalesMicroservice.API.Endpoints.Sales.Create;

namespace SaleMicroservice.API.Endpoints.Sales.Create;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResponse>
{
    private readonly SalesDbContext _context;
    private readonly IValidator<CreateSaleRequest> _validator;

    public CreateSaleHandler(
        SalesDbContext context,
        IValidator<CreateSaleRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<CreateSaleResponse> Handle(CreateSaleCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command.Request, ct);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var items = command.Request.Items.Select(i => new SaleItem(
            i.ProductId,
            i.Quantity,
            i.UnitPrice,
            i.Warranty != null
                ? new Warranty(i.Warranty.Value)
                : null
        )).ToList();

        var sale = new Sale(items);

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(ct);

        return new CreateSaleResponse(sale.Id);
    }
}