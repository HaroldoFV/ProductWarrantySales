using ProductMicroservice.Application.Common;
using ProductMicroservice.Application.UseCases.Product.Common;

namespace ProductMicroservice.Application.UseCases.Product.ListProducts;

public class ListProductsOutput
    : PaginatedListOutput<ProductModelOutput>
{
    public ListProductsOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<ProductModelOutput> items)
        : base(page, perPage, total, items)
    {
    }
}