using GraphQL.Types;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.GraphQL.Types
{
    public class BasketType: ObjectGraphType<BasketModel>
    {
        public BasketType()
        {
            Field(f => f.UserName, type: typeof(StringGraphType)).Description("User Name");
            Field(f => f.TotalPrice, type: typeof(DecimalGraphType)).Description("Total Price");
            Field(f => f.Items, type: typeof(ListGraphType<BasketItemExtendedType>)).Description("Basket Items");
        }
    }
}
