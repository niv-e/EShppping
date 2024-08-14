using Catalog.Application.Response;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductsQuery(CatalogSpecParams catalogSpecParams) : IRequest<Pagination<ProductResponse>>
{
    public CatalogSpecParams catalogSpecParams { init; get; } = catalogSpecParams;
}