using GraphQL;
using GraphQL.Types;
using Shopping.Aggregator.GraphQL.Types;
using Shopping.Aggregator.Repositories;
using System;

namespace Shopping.Aggregator.GraphQL.Queries
{
    public class ShoppingQuery : ObjectGraphType
    {
        private readonly IShoppingRepository _shoppingRepository;
        public ShoppingQuery(IShoppingRepository shoppingRepository)
        {
            _shoppingRepository = shoppingRepository;

            // Fields
            FieldAsync<BasketType>(
               name: "basket",
               description: "Basket With Products",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" }),
               resolve: async context =>
               {
                   try
                   {
                       string userName = context.GetArgument<string>("username");
                       var basket = await _shoppingRepository.GetBasket(userName);
                       return basket;
                   }
                   catch (Exception ex)
                   {
                       throw new ExecutionError(ex.Message);
                   }
               });

            FieldAsync<ListGraphType<OrderResponseType>>(
                name: "orders",
                description: "Orders",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" }),
               resolve: async context =>
               {
                   try
                   {
                       string userName = context.GetArgument<string>("username");
                       var basket = await _shoppingRepository.GetOrders(userName);
                       return basket;
                   }
                   catch (Exception ex)
                   {
                       throw new ExecutionError(ex.Message);
                   }
               });
        }
    }
}
