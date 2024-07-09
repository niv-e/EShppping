using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productsRepostory;

    public GetProductByIdHandler(IProductRepository productsRepostory)
    {
        _productsRepostory = productsRepostory;
    }


    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productsRepostory.GetProductById(request.id)
            ?? throw new NullReferenceException($"Could not find product with id: { request.id }");
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(product);
        return productResponse;
    }
}

