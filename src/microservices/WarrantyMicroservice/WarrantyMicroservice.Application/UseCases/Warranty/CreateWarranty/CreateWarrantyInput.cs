using MediatR;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;

namespace WarrantyMicroservice.Application.UseCases.Warranty.CreateWarranty;

public record CreateWarrantyInput(
    string Name,
    decimal Value,
    int TermInYears) : IRequest<WarrantyModelOutput>;