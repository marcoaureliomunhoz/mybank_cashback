using GraphQL.Types;
using MyBank.Cashback.Domain.Entities;

namespace MyBank.Cashback.Infra.GraphQL.Types
{
    public class TransactionType : ObjectGraphType<Transaction>
    {
        public TransactionType()
        {
            Field(x => x.TransactionId, type: typeof(IdGraphType));
            Field(x => x.Description);
            Field(x => x.RegisterDate);
            Field(x => x.Value);
            Field(x => x.ClientId);
        }
    }
}
