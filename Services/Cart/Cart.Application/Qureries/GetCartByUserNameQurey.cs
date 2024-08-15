using Cart.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Qureries;

public record GetCartByUserNameQurey(string UserName) : IRequest<ShoppingCartResponse>
{
}
