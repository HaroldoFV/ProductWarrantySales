using SaleMicroservice.API.Common.Exceptions;
using SalesMicroservice.API.Crosscutting;

namespace SaleMicroservice.API.Endpoints.Sales.Domain;

public class Warranty : Entity
{
    public decimal Value { get; private set; }

    private Warranty()
    {
    }

    public Warranty(decimal value)
    {
        Value = value;
        Validate();
    }

    private void Validate()
    {
        if (Value <= 0)
            throw new DomainValidationException("Warranty value must be greater than zero");
    }
}