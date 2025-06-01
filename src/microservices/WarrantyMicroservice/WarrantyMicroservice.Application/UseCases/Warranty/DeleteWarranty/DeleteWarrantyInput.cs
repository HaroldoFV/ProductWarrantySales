using MediatR;

namespace WarrantyMicroservice.Application.UseCases.Warranty.DeleteWarranty;

public class DeleteWarrantyInput : IRequest
{
    public Guid Id { get; set; }

    public DeleteWarrantyInput(Guid id)
        => Id = id;
}