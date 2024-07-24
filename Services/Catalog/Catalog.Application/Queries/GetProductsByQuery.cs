using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductsByQuery : IRequest<IList<ProductResponse>>
{
    public string? BrandName { init; get; }
    public string? ProductName { init; get; }

}