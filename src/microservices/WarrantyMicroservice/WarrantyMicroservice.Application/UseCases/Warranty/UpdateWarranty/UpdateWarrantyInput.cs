using MediatR;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;

namespace WarrantyMicroservice.Application.UseCases.Warranty.UpdateWarranty;

public record UpdateWarrantyInput(
    Guid Id,
    string Name,
    decimal Value,
    int TermInYears) : IRequest<WarrantyModelOutput>;