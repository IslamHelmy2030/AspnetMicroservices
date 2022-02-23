using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Repositories
{
    public interface IShoppingRepository
    {
        Task<ShoppingModel> GetShopping(string userName);
        Task<BasketModel> GetBasket(string userName);
        Task<IEnumerable<OrderResponseModel>> GetOrders(string userName);
    }
}
