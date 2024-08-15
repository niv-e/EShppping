using Cart.Application.Commands;
using Cart.Application.Qureries;
using Cart.Application.Responses;
using Cart.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cart.API.Controllers;


public class CartController : ApiController
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(ShoppingCartResponse),(int) HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> GetCartByUserName(string userName)
    {
        var getCartByUserNameQuery = new GetCartByUserNameQurey(userName);
        var shoppingCartResponse = await _mediator.Send(getCartByUserNameQuery);
        return Ok(shoppingCartResponse);
    }


    [HttpPost("{userName}")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> CreateCartByUserName(
        string userName, 
        [FromBody]IList<ShoppingCartItem> items)
    {
        var createCartByUserNameQuery = new CreateShoppingCartCommand(userName, items);
        var shoppingCartResponse = await _mediator.Send(createCartByUserNameQuery);
        return Ok(shoppingCartResponse);
    }


    [HttpDelete("{userName}")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> DeleteCartByUserName(
        string userName)
    {
        var deleteCartByUserNameQuery = new DeleteCartByUserNameCommand(userName);
        await _mediator.Send(deleteCartByUserNameQuery);
        return Ok();
    }
}
