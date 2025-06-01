using MediatR;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;

namespace WarrantyMicroservice.Application.UseCases.Warranty.GetWarranty;

public class GetWarrantyInput : IRequest<WarrantyModelOutput>
{
    public Guid Id { get; set; }

    public GetWarrantyInput(Guid id)
        => Id = id;
}