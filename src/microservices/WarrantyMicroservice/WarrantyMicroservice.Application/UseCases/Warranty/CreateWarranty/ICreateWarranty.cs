using MediatR;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;

namespace WarrantyMicroservice.Application.UseCases.Warranty.CreateWarranty;

public interface ICreateWarranty :
    IRequestHandler<CreateWarrantyInput, WarrantyModelOutput>
{
}