using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Core.Entities;
public record ShoppingCartItem(string productId, string productName)
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; } = productId;
    public string? ImageFile { get; set; } = null;
    public string ProductName { get; set; } = productName;

}

