using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Core.Entities;
public record ShoppingCartItem(string ProductId, string ProductName)
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? ImageFile { get; set; } = null;
}

