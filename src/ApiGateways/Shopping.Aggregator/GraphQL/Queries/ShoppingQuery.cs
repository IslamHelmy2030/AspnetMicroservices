using GraphQL;
using GraphQL.Types;
using Shopping.Aggregator.GraphQL.Types;
using Shopping.Aggregator.Repositories;
using System;

namespace Shopping.Aggregator.GraphQL.Queries
{
    public class ShoppingQuery : ObjectGraphType
    {
        public ShoppingQuery(IShoppingRepository shoppingRepository)
        {
            // Fields
            FieldAsync<ShoppingType>(
               name: "shopping",
               description: "Basket With Products and Orders",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" }),
               resolve: async context =>
               {
                   try
                   {
                       string userName = context.GetArgument<string>("username");
                       var shopping = await shoppingRepository.GetShopping(userName);
                       return shopping;
                   }
                   catch (Exception ex)
                   {
                       throw new ExecutionError(ex.Message);
                   }
               });
        }
    }
}
