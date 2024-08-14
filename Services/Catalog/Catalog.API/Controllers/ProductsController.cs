using Catalog.Application.Response;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Catalog.Application.Queries;
using System.Net;
using Catalog.Application.Commands;
using Catalog.Core.Specs;

namespace Catalog.API.Controllers;

public class ProductsController : ApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), ((int)HttpStatusCode.OK))]
    public async Task<ActionResult<ProductResponse>> GetProductById(int id)
    {
        var getProductByIdQuery = new GetProductByIdQuery(id.ToString());
        var results = await _mediator.Send(getProductByIdQuery);
        return Ok(results);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductResponse>), ((int)HttpStatusCode.OK))]
    public async Task<ActionResult<ProductResponse>> GetProductsBy(
        [FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var getProdcutsByQuery = new GetProductsQuery(catalogSpecParams);

        var results = await _mediator.Send(getProdcutsByQuery);
        return Ok(results);
    }


    [HttpPost()]
    [ProducesResponseType(typeof(ProductResponse), ((int)HttpStatusCode.Created))]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody]CreateProductCommand createProductCommand)
    {
        var results = await _mediator.Send(createProductCommand);
        return Ok(results);
    }

    [HttpPut()]
    [ProducesResponseType(typeof(ProductResponse), ((int)HttpStatusCode.OK))]
    public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
    {
        var results = await _mediator.Send(updateProductCommand);
        return Ok(results);
    }

    [HttpDelete]
    [Route("{id}", Name = "DeleteProductById")]
    [ProducesResponseType(typeof(ProductResponse), ((int)HttpStatusCode.OK))]
    public async Task<ActionResult<ProductResponse>> DeleteProductById(int id)
    {
        var deleteProductByIdQuery = new DeleteProductByIdCommand(id.ToString());
        var results = await _mediator.Send(deleteProductByIdQuery);
        return Ok(results);
    }
}
