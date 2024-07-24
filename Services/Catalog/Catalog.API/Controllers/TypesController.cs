using Catalog.Application.Queries;
using Catalog.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

public class TypesController : ApiController
{
    private readonly IMediator _mediator;

    public TypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<TypeResponse>), ((int)HttpStatusCode.OK))]
    public async Task<ActionResult<IList<TypeResponse>>> GetAllTypes()
    {
        var getAllTypesQuery = new GetAllTypesQuery();
        var results = await _mediator.Send(getAllTypesQuery);
        return Ok(results);
    }
}

