using SaleMicroservice.API.Common.Exceptions;
using SalesMicroservice.API.Crosscutting;

namespace SaleMicroservice.API.Endpoints.Sales.Domain;

public class Sale : Entity
{
    public IReadOnlyList<SaleItem> Items => _items.AsReadOnly();
    public decimal TotalAmount { get; private set; }

    private readonly List<SaleItem> _items;

    private Sale()
    {
        _items = new();
    }

    public Sale(List<SaleItem> items)
    {
        _items = items;
        CreatedAt = DateTime.UtcNow;
        CalculateTotalAmount();
        Validate();
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalAmount);
    }

    private void Validate()
    {
        if (!_items.Any())
            throw new DomainValidationException("Sale must have at least one item");
    }
}