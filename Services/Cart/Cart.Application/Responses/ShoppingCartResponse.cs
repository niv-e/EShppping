using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Responses;

public record ShoppingCartResponse
{
    public required string UserName { get; init; }
    public IEnumerable<ShoppingCartItemResponse> Items { get; init; } = Enumerable.Empty<ShoppingCartItemResponse>();
    public decimal TotalPrice { get; init; }

    public ShoppingCartResponse() { }
    public ShoppingCartResponse(string userName)
    {
        UserName = userName;
    }
}
