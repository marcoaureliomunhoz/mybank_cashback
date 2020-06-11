using GraphQL.Types;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Services;
using MyBank.Cashback.Infra.GraphQL.Types;
using System.Threading.Tasks;

namespace MyBank.Cashback.Infra.GraphQL.Mutations
{
    public class TransactionMutation : ObjectGraphType<object>
    {
        public TransactionMutation(ITransactionService service)
        {
            Field<TransactionType>(
                "addTransaction",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AddTransactionRequest>>() { Name = "transaction" }),
                resolve: context => CreateTransaction(context, service)
            );
        }

        public async Task<Transaction> CreateTransaction(
            ResolveFieldContext<object> context,
            ITransactionService service)
        {
            var addTransactionRequest = context.GetArgument<AddTransactionRequest>("transaction");
            var transaction = new Transaction
            {
                Description = addTransactionRequest.Description,
                Value = addTransactionRequest.Value,
                ClientId = addTransactionRequest.ClientId
            };
            await service.Insert(transaction);
            return transaction;
        }
    }
}
