using WarrantyMicroservice.Application.Interfaces;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;
using WarrantyMicroservice.Domain.Repository;

namespace WarrantyMicroservice.Application.UseCases.Warranty.UpdateWarranty;

public class UpdateWarranty : IUpdateWarranty
{
    public UpdateWarranty(IWarrantyRepository warrantyRepository, IUnitOfWork unitOfWork)
    {
        _warrantyRepository = warrantyRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IWarrantyRepository _warrantyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<WarrantyModelOutput> Handle(UpdateWarrantyInput request, CancellationToken cancellationToken)
    {
        var warranty = await _warrantyRepository.GetAsync(request.Id, cancellationToken);
        warranty.Update(
            request.Name,
            request.Value,
            request.TermInYears
        );

        await _warrantyRepository.UpdateAsync(warranty, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new WarrantyModelOutput(warranty);
    }
}