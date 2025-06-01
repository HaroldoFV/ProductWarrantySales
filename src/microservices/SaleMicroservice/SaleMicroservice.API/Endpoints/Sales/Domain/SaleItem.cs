using SaleMicroservice.API.Common.Exceptions;
using SalesMicroservice.API.Crosscutting;

namespace SaleMicroservice.API.Endpoints.Sales.Domain;

public class SaleItem : Entity
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalAmount { get; private set; }
    public Warranty? Warranty { get; private set; }

    private SaleItem()
    {
    }

    public SaleItem(Guid productId, int quantity, decimal unitPrice, Warranty? warranty = null)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Warranty = warranty;

        CalculateTotalAmount();
        Validate();
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = Quantity * UnitPrice;
        if (Warranty != null)
            TotalAmount += Warranty.Value;
    }

    private void Validate()
    {
        if (Quantity <= 0)
            throw new DomainValidationException("Quantity must be greater than zero");
        if (UnitPrice <= 0)
            throw new DomainValidationException("Unit price must be greater than zero");
    }
}