using MediatR;

namespace WarrantyMicroservice.Application.UseCases.Warranty.ListWarranties;

public interface IListWarranties
    : IRequestHandler<ListWarrantiesInput, ListWarrantiesOutput>
{
}