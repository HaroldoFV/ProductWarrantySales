namespace WarrantyMicroservice.Application.UseCases.Warranty.Common;

public record WarrantyModelOutput(
    Guid Id,
    string Name,
    decimal Value,
    int TermInYears)
{
    public WarrantyModelOutput(Domain.Entity.Warranty warranty) : this(
        warranty.Id,
        warranty.Name,
        warranty.Value,
        warranty.TermInYears)
    {
    }
}