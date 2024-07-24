using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

public class BrandsController : ApiController
{
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<BrandResponse>), ((int)HttpStatusCode.OK))]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var getAllBrandsQuery = new GetAllBrandsQuery();
        var results = await _mediator.Send(getAllBrandsQuery);
        return Ok(results);
    }
}

