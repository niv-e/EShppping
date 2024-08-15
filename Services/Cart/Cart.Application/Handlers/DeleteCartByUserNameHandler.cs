using Cart.Application.Commands;
using Cart.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Handlers;

public class DeleteCartByUserNameHandler : IRequestHandler<DeleteCartByUserNameCommand>
{
    private readonly ICartRepository _cartRepository;

    public DeleteCartByUserNameHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task Handle(DeleteCartByUserNameCommand request, CancellationToken cancellationToken)
    {
        await _cartRepository.DeleteShoppingCart(request.UserName, cancellationToken);
    }
}
