using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Repositories
{
    public class ShoppingRepository : IShoppingRepository
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingRepository(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<ShoppingModel> GetShopping(string userName)
        {
            //Get Busket by username
            var basket = await _basketService.GetBasket(userName);

            //Get product details foreach item in basket
            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);

                // set additional product fields
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            //retrieve order list by username
            var orders = await _orderService.GetOrdersByUserName(userName);

            //Return the shoppingModel Dto including all data
            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return shoppingModel;
        }
    }
}
