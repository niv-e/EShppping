using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Responses;

public record ShoppingCartItemResponse
{
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public required string ProductId { get; init; }
    public string? ImageFile { get; init; }
    public required string ProductName { get; init; }
}