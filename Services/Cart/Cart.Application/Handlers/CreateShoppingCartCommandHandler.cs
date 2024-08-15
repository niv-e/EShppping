using Cart.Application.Responses;
using Cart.Core.Repository;
using Cart.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Core.Entities;
using Catalog.Application.Mappers;

namespace Cart.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly ICartRepository _cartRepository;

    public CreateShoppingCartCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        new ShoppingCart(request.UserName);
        //TODO: Call discount service and apply coupons
        var shoppingCart = await _cartRepository.UpdateShoppingCart(
            new ShoppingCart(request.UserName, request.Items),cancellationToken);
        var shoppingCartResponse = CartMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;

    }
}
