using WarrantyMicroservice.Application.UseCases.Warranty.Common;
using WarrantyMicroservice.Domain.Repository;

namespace WarrantyMicroservice.Application.UseCases.Warranty.GetWarranty;

public class GetWarranty : IGetWarranty
{
    private readonly IWarrantyRepository _warrantyRepository;

    public GetWarranty(IWarrantyRepository warrantyRepository)
    {
        _warrantyRepository = warrantyRepository;
    }
    
    public async Task<WarrantyModelOutput> Handle(
        GetWarrantyInput request,
        CancellationToken cancellationToken
    )
    {
        var product = await _warrantyRepository.GetAsync(request.Id, cancellationToken);
        return new WarrantyModelOutput(product);
    }
}