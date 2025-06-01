using MediatR;
using WarrantyMicroservice.Application.Common;
using WarrantyMicroservice.Domain.SeedWork.SearchableRepository;

namespace WarrantyMicroservice.Application.UseCases.Warranty.ListWarranties;

public class ListWarrantiesInput
    : PaginatedListInput, IRequest<ListWarrantiesOutput>
{
    public ListWarrantiesInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc
    ) : base(page, perPage, search, sort, dir)
    {
    }

    public ListWarrantiesInput()
        : base(1, 15, "", "", SearchOrder.Asc)
    {
    }
}