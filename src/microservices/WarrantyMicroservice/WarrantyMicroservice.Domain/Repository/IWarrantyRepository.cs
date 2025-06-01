using WarrantyMicroservice.Domain.Entity;
using WarrantyMicroservice.Domain.SeedWork;
using WarrantyMicroservice.Domain.SeedWork.SearchableRepository;

namespace WarrantyMicroservice.Domain.Repository;

public interface IWarrantyRepository
    : IGenericRepository<Warranty>
        , ISearchableRepository<Warranty>
{
}