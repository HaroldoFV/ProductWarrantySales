using MediatR;

namespace WarrantyMicroservice.Application.UseCases.Warranty.DeleteWarranty;

public interface IDeleteProduct
    : IRequestHandler<DeleteWarrantyInput>
{
}