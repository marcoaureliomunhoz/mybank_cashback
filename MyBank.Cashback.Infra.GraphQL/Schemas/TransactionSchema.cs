using GraphQL;
using GraphQL.Types;
using MyBank.Cashback.Infra.GraphQL.Mutations;
using MyBank.Cashback.Infra.GraphQL.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Cashback.Infra.GraphQL.Schemas
{
    public class TransactionSchema : Schema
    {
        public TransactionSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TransactionQuery>();
            Mutation = resolver.Resolve<TransactionMutation>();
        }
    }
}
