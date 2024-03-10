namespace Basket.API.Repositories
{
    public class BasketRepository(IDistributedCache redisCache) 
        : IBasketRepository
    {
        public async Task<ShoppingCart?> GetBasket(string userName)
        {
            var basket = await redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
        {
            await redisCache.SetStringAsync(
                basket.UserName,
                JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }
    }
}
