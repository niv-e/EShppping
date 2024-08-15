using AutoMapper;
using Cart.Application.Qureries;
using Cart.Application.Responses;
using Cart.Core.Entities;
using Cart.Core.Repository;
using Catalog.Application.Mappers;
using MediatR;

namespace Cart.Application.Handlers;

public class GetCartByUserNameHandler : IRequestHandler<GetCartByUserNameQurey, ShoppingCartResponse>
{
    private readonly ICartRepository _cartRepository;

    public GetCartByUserNameHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    public async Task<ShoppingCartResponse> Handle(GetCartByUserNameQurey request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _cartRepository.GetShoppingCart(request.UserName,cancellationToken);
        var shoppingCartResponse = CartMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }

}
