
using MediatR;

namespace ProductMicroservice.Application.UseCases.Product.DeleteProduct;
public class DeleteProductInput : IRequest
{
    public Guid Id { get; set; }
    public DeleteProductInput(Guid id) 
        => Id = id;
}
