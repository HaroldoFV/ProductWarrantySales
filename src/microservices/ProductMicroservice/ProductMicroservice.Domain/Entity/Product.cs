using ProductMicroservice.Domain.SeedWork;
using ProductMicroservice.Domain.Validation;

namespace ProductMicroservice.Domain.Entity;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int MinimumStock { get; private set; }
    public int MaximumStock { get; private set; }
    public int Stock { get; private set; }
    public string Supplier { get; private set; }
    public bool HasWarranty { get; private set; }

    public Product(
        string name,
        decimal price,
        int minimumStock,
        int maximumStock,
        int stock,
        string supplier,
        bool hasWarranty)
    {
        Name = name;
        Price = price;
        MinimumStock = minimumStock;
        MaximumStock = maximumStock;
        Stock = stock;
        Supplier = supplier;
        HasWarranty = hasWarranty;

        Validate();
    }


    public void UpdateStock(int quantity)
    {
        if (quantity < 0 && Math.Abs(quantity) > Stock)
            throw new InvalidOperationException("Insufficient stock to reduce.");

        Stock += quantity;

        Validate();
    }


    #region Validation

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, 3, nameof(Name));
        DomainValidation.MaxLength(Name, 255, nameof(Name));

        DomainValidation.GreaterThan(Price, 0, nameof(Price));

        DomainValidation.GreaterOrEqualThan(MinimumStock, 0, nameof(MinimumStock));
        DomainValidation.GreaterThan(MaximumStock, MinimumStock, nameof(MaximumStock));

        DomainValidation.GreaterOrEqualThan(Stock, 0, nameof(Stock));
        DomainValidation.LessOrEqualThan(Stock, MaximumStock, nameof(Stock));

        DomainValidation.NotNullOrEmpty(Supplier, nameof(Supplier));
        DomainValidation.MinLength(Supplier, 2, nameof(Supplier));
        DomainValidation.MaxLength(Supplier, 100, nameof(Supplier));
    }

    #endregion
}