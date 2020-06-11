using GraphQL.Types;

namespace MyBank.Cashback.Infra.GraphQL.Mutations
{
    public class AddTransactionRequest : InputObjectGraphType
    {
        public decimal Value { get; set; }
        public int ClientId { get; set; }

        public AddTransactionRequest()
        {
            Name = "TransactionRequest";
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<NonNullGraphType<DecimalGraphType>>("value");
            Field<NonNullGraphType<IntGraphType>>("clientid");
        }
    }
}
