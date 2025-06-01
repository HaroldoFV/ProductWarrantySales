using WarrantyMicroservice.Domain.SeedWork;
using WarrantyMicroservice.Domain.Validation;

namespace WarrantyMicroservice.Domain.Entity;

public class Warranty : AggregateRoot
{
    public string Name { get; private set; }
    public decimal Value { get; private set; }
    public int TermInYears { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdated { get; private set; }

    public Warranty(string name, decimal value, int termInYears)
    {
        Name = name;
        Value = value;
        TermInYears = termInYears;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public void Update(string name, decimal value, int termInYears)
    {
        Name = name;
        Value = value;
        TermInYears = termInYears;
        LastUpdated = DateTime.Now;

        Validate();
    }

    #region Validation

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, 3, nameof(Name));
        DomainValidation.MaxLength(Name, 255, nameof(Name));

        DomainValidation.GreaterThan(Value, 0, nameof(Value));
        DomainValidation.GreaterThan(TermInYears, 0, nameof(TermInYears));
    }

    #endregion
}