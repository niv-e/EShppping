using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsByHandler : IRequestHandler<GetProductsByQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productsRepostory;

    public GetProductsByHandler(IProductRepository productsRepostory)
    {
        _productsRepostory = productsRepostory;
    }


    public async Task<IList<ProductResponse>> Handle(GetProductsByQuery qurey, CancellationToken cancellationToken)
    {

        var filter = ProductMapper.Mapper.Map<ProductsFilter>(qurey);
        var products = await _productsRepostory.GetProductsBy(filter);
        var productsResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(products.ToList());
        return productsResponse;
    }
}

