using WarrantyMicroservice.Application.UseCases.Warranty.Common;
using WarrantyMicroservice.Domain.Repository;

namespace WarrantyMicroservice.Application.UseCases.Warranty.ListWarranties;

public class ListWarranties : IListWarranties
{
    private readonly IWarrantyRepository _warrantyRepository;

    public ListWarranties(IWarrantyRepository warrantyRepository)
    {
        _warrantyRepository = warrantyRepository;
    }

    public async Task<ListWarrantiesOutput> Handle(
        ListWarrantiesInput request,
        CancellationToken cancellationToken)
    {
        var searchOutput = await _warrantyRepository.Search(
            new(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
            ),
            cancellationToken
        );

        return new ListWarrantiesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(warranty => new WarrantyModelOutput(
                    warranty
                ))
                .ToList()
        );
    }
}