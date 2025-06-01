using WarrantyMicroservice.Application.Interfaces;
using WarrantyMicroservice.Domain.Repository;

namespace WarrantyMicroservice.Application.UseCases.Warranty.DeleteWarranty;

public class DeleteWarranty : IDeleteProduct
{
    public DeleteWarranty(IWarrantyRepository warrantyRepository, IUnitOfWork unitOfWork)
    {
        _warrantyRepository = warrantyRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IWarrantyRepository _warrantyRepository;
    private readonly IUnitOfWork _unitOfWork;


    public async Task Handle(DeleteWarrantyInput request, CancellationToken cancellationToken)
    {
        var product = await _warrantyRepository.GetAsync(request.Id, cancellationToken);

        await _warrantyRepository.DeleteAsync(product, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}