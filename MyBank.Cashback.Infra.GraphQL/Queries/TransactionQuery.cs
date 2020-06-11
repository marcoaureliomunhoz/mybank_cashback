using GraphQL.Types;
using MyBank.Cashback.Domain.Interface.Repositories;
using MyBank.Cashback.Infra.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Cashback.Infra.GraphQL.Queries
{
    public class TransactionQuery : ObjectGraphType
    {
        public TransactionQuery(ITransactionRepository repository)
        {
            Field<ListGraphType<TransactionType>>(
                "transactions",
                resolve: context => repository.ListAll()
            );
        }
    }
}
