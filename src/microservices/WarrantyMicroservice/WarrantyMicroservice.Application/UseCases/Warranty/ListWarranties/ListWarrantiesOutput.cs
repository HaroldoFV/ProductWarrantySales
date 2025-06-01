using WarrantyMicroservice.Application.Common;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;

namespace WarrantyMicroservice.Application.UseCases.Warranty.ListWarranties;

public class ListWarrantiesOutput
    : PaginatedListOutput<WarrantyModelOutput>
{
    public ListWarrantiesOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<WarrantyModelOutput> items)
        : base(page, perPage, total, items)
    {
    }
}