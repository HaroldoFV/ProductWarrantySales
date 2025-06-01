namespace WarrantyMicroservice.API.ApiModels.Warranty;

public record UpdateWarrantyApiInput(
    string Name,
    decimal Value,
    int TermInYears
);