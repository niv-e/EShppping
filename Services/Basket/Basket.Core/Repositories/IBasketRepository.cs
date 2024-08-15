using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Repository;

public interface IBasketRepository
{
    Task<ShoppingCart> GetShoppingCart(string userName, CancellationToken cancellationToken);
    Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken);
    Task DeleteShoppingCart(string userName, CancellationToken cancellationToken);
}
