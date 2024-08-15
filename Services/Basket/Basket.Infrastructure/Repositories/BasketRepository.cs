using Basket.Core.Entities;
using Basket.Core.Repository;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
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
                shoppingCart.userName, 
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
