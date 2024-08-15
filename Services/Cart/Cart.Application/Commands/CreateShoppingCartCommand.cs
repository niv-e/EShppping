using Cart.Application.Responses;
using Cart.Core.Entities;
using MediatR;

namespace Cart.Application.Commands;

public record CreateShoppingCartCommand(string UserName) : IRequest<ShoppingCartResponse>
{
    public CreateShoppingCartCommand(string userName, IEnumerable<ShoppingCartItem> items) : this(userName)
    {
        Items = items;
    }
    public IEnumerable<ShoppingCartItem> Items { get; set; } = Enumerable.Empty<ShoppingCartItem>();    
}
