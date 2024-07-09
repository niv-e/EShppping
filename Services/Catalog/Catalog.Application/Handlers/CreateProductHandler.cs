using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productsRepostory;

    public CreateProductHandler(IProductRepository productsRepostory)
    {
        _productsRepostory = productsRepostory;
    }


    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = ProductMapper.Mapper.Map<Product>(request) ?? 
            throw new ApplicationException("Something when wrong while creating the product");

        var product = await _productsRepostory.CreateProduct(productEntity);
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(product);
        return productResponse;

    }
}

