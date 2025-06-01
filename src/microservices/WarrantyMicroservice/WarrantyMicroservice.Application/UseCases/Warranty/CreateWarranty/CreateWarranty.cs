using WarrantyMicroservice.Application.Interfaces;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;
using WarrantyMicroservice.Domain.Repository;


namespace WarrantyMicroservice.Application.UseCases.Warranty.CreateWarranty;

public class CreateWarranty : ICreateWarranty
{
    public CreateWarranty(IWarrantyRepository warrantyRepository, IUnitOfWork unitOfWork)
    {
        _warrantyRepository = warrantyRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IWarrantyRepository _warrantyRepository;
    private readonly IUnitOfWork _unitOfWork;


    public async Task<WarrantyModelOutput> Handle(
        CreateWarrantyInput input,
        CancellationToken cancellationToken)
    {
        var warranty = new Domain.Entity.Warranty(
            input.Name,
            input.Value,
            input.TermInYears
        );
        await _warrantyRepository.InsertAsync(warranty, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new WarrantyModelOutput(warranty);
    }
}