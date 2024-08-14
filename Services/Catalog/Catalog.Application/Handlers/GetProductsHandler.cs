using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productsRepostory;
    private readonly ILogger<GetProductsHandler> _logger;

    public GetProductsHandler(IProductRepository productsRepostory, ILogger<GetProductsHandler> logger)
    {
        _productsRepostory = productsRepostory;
        _logger = logger;
    }

    public async Task<Pagination<ProductResponse>> Handle(GetProductsQuery qurey, CancellationToken cancellationToken)
    {

        var products = await _productsRepostory.GetProducts(qurey.catalogSpecParams);
        var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(products);
        _logger.LogDebug("Received Product List.Total Count: {productList}", productResponseList.Count);
        return productResponseList;
    }
}

