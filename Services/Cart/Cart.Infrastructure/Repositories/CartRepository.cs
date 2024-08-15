using Cart.Core.Entities;
using Cart.Core.Repository;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cart.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        public async Task<ShoppingCart> GetShoppingCart(string userName, CancellationToken cancellationToken)
        {
            var cart = await _redisCache.GetStringAsync(userName, cancellationToken);
            
            return string.IsNullOrEmpty(cart) ?
                new ShoppingCart(userName):
                JsonSerializer.Deserialize<ShoppingCart>(cart) ?? throw new SerializationException();
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            await _redisCache.SetStringAsync(
                shoppingCart.UserName, 
                JsonSerializer.Serialize(shoppingCart),
                cancellationToken);

            return shoppingCart;
        }
        public async Task DeleteShoppingCart(string userName, CancellationToken cancellationToken)
        {
            await _redisCache.RemoveAsync(userName, cancellationToken);
        }




    }
}
