using GraphQL.Types;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.GraphQL.Types
{
    public class BasketItemExtendedType : ObjectGraphType<BasketItemExtendedModel>
    {
        public BasketItemExtendedType()
        {
            Field(f => f.Quantity, type: typeof(IntGraphType)).Description("Quantity");
            Field(f => f.Color, type: typeof(StringGraphType)).Description("Color");
            Field(f => f.Price, type: typeof(DecimalGraphType)).Description("Price");
            Field(f => f.ProductId, type: typeof(StringGraphType)).Description("Product Id");
            Field(f => f.ProductName, type: typeof(StringGraphType)).Description("Product Name");
            Field(f => f.Category, type: typeof(StringGraphType)).Description("Category");
            Field(f => f.Summary, type: typeof(StringGraphType)).Description("Summary");
            Field(f => f.Description, type: typeof(StringGraphType)).Description("Description");
            Field(f => f.ImageFile, type: typeof(StringGraphType)).Description("Image File");
        }
    }
}
