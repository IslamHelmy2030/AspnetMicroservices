using GraphQL.Types;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.GraphQL.Types
{
    public class OrderResponseType: ObjectGraphType<OrderResponseModel>
    {
        public OrderResponseType()
        {
            Field(f => f.UserName, type: typeof(StringGraphType)).Description("User Name");
            Field(f => f.TotalPrice, type: typeof(DecimalGraphType)).Description("Total Price");

            // BillingAddress
            Field(f => f.FirstName, type: typeof(StringGraphType)).Description("First Name");
            Field(f => f.LastName, type: typeof(StringGraphType)).Description("Last Name");
            Field(f => f.EmailAddress, type: typeof(StringGraphType)).Description("EmailAddress");
            Field(f => f.AddressLine, type: typeof(StringGraphType)).Description("Address Line");
            Field(f => f.Country, type: typeof(StringGraphType)).Description("Country");
            Field(f => f.State, type: typeof(StringGraphType)).Description("State");
            Field(f => f.ZipCode, type: typeof(StringGraphType)).Description("ZipCode");

            // Payment
            Field(f => f.CardName, type: typeof(StringGraphType)).Description("Card Name");
            Field(f => f.CardNumber, type: typeof(StringGraphType)).Description("Card Number");
            Field(f => f.Expiration, type: typeof(StringGraphType)).Description("Expiration");
            Field(f => f.CVV, type: typeof(StringGraphType)).Description("CVV");
            Field(f => f.PaymentMethod, type: typeof(IntGraphType)).Description("Payment Method");
        }
    }
}
