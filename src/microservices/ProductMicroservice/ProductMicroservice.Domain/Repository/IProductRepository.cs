using ProductMicroservice.Domain.Entity;
using ProductMicroservice.Domain.SeedWork;
using ProductMicroservice.Domain.SeedWork.SearchableRepository;

namespace ProductMicroservice.Domain.Repository;

public interface IProductRepository
    : IGenericRepository<Product>
        , ISearchableRepository<Product>
{
}