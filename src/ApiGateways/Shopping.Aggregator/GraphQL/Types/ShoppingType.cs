using GraphQL.Types;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.GraphQL.Types
{
    public class ShoppingType: ObjectGraphType<ShoppingModel>
    {
        public ShoppingType()
        {
            Field(f => f.BasketWithProducts, type: typeof(BasketType)).Description("Basket With Products");
            Field(f => f.Orders, type: typeof(ListGraphType<OrderResponseType>)).Description("Orders");
        }
    }
}
