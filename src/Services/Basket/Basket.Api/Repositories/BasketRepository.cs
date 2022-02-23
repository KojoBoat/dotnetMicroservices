using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this.redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteCart(string userName)
        {
            if(string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
            var cart = await redisCache.GetStringAsync(userName);
            if(cart != null) await redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetCart(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
            var cart = await redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(cart)) return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(cart);
        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart.UserName == null) throw new ArgumentNullException(nameof(shoppingCart.UserName));
            var getUserCart = await redisCache.GetStringAsync(shoppingCart.UserName);

            await redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));

            return await GetCart(shoppingCart.UserName);

        }
    }
}
