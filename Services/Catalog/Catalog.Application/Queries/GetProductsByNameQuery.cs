using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductsByNameQuery : IRequest<IList<ProductResponse>>
{
    public required string Name { init; get; }
}
