using MediatR;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;

namespace WarrantyMicroservice.Application.UseCases.Warranty.UpdateWarranty;
public interface IUpdateWarranty
    : IRequestHandler<UpdateWarrantyInput, WarrantyModelOutput>
{}
