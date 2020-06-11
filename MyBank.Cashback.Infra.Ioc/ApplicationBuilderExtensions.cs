using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using MyBank.Cashback.Infra.GraphQL.Schemas;


namespace MyBank.Cashback.Infra.Ioc
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMyBankConfiguration(this IApplicationBuilder app)
        {
            app.UseGraphQL<TransactionSchema>("/ql/transaction");
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            return app;
        }
    }
}
