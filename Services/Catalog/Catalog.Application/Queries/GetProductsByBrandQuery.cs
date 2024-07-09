using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductsByBrandQuery : IRequest<IList<ProductResponse>>
{
    public required string BrandName { init; get; }
}
