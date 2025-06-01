using MediatR;
using WarrantyMicroservice.Application.UseCases.Warranty.Common;

namespace WarrantyMicroservice.Application.UseCases.Warranty.GetWarranty;

public interface IGetWarranty : IRequestHandler<GetWarrantyInput, WarrantyModelOutput>;