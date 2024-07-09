using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsByNameHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productsRepostory;

    public GetProductsByNameHandler(IProductRepository productsRepostory)
    {
        _productsRepostory = productsRepostory;
    }


    public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _productsRepostory.GetProductsByName(request.Name);
        var productsResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(products.ToList());
        return productsResponse;
    }
}

