using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsByBrandHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productsRepostory;

    public GetProductsByBrandHandler(IProductRepository productsRepostory)
    {
        _productsRepostory = productsRepostory;
    }


    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var products = await _productsRepostory.GetProductsByBrand(request.BrandName);
        var productsResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(products.ToList());
        return productsResponse;
    }
}

