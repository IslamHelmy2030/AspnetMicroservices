using GraphQL.Types;
using Shopping.Aggregator.GraphQL.Queries;

namespace Shopping.Aggregator.GraphQL.Schemas
{
    public class ShoppingSchema : Schema
    {
        public ShoppingSchema(ShoppingQuery shoppingQuery)
        {
            Query = shoppingQuery;
        }
    }
}
