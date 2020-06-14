using GraphQL;
using GraphQL.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBank.Cashback.Domain.Interface.Repositories;
using MyBank.Cashback.Domain.Interface.Services;
using MyBank.Cashback.Domain.Services;
using MyBank.Cashback.Infra.Data.Repositories;
using MyBank.Cashback.Infra.GraphQL.Mutations;
using MyBank.Cashback.Infra.GraphQL.Queries;
using MyBank.Cashback.Infra.GraphQL.Schemas;
using MyBank.Cashback.Infra.GraphQL.Types;
using MyBank.Infra.Generics.Extensions;
using MyBank.Infra.Generics.Interfaces;
using MyBank.Infra.Generics.Providers;
using MyBank.Infra.Generics.Services;
using Serilog;

namespace MyBank.Cashback.Infra.Ioc
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMyBankInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var stringConexao = configuration?.GetConnectionString("MyBank_Cashback") ?? "";
            System.Console.WriteLine($"stringConexao: {stringConexao}");

            return services
                .ConfigureRepositories()
                .ConfigureDomainServices()
                .ConfigureGraphQL()
                .ConfigureProviders()
                .ConfigureKafka(configuration)
                .ConfigureLogging();
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICashbackRepository, CashbackRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }

        private static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICashbackService, CashbackService>();

            return services;
        }

        private static IServiceCollection ConfigureGraphQL(this IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s =>
                new FuncDependencyResolver(s.GetRequiredService));

            // Types
            services.AddScoped<TransactionType>();

            // Querys
            services.AddScoped<TransactionQuery>();

            // Mutations
            services.AddScoped<AddTransactionRequest>();
            services.AddScoped<TransactionMutation>();

            // Schemas
            services.AddScoped<TransactionSchema>();

            services.AddGraphQL(o => {
                o.ExposeExceptions = true;
                o.EnableMetrics = true;
            }).AddGraphTypes(ServiceLifetime.Scoped);

            return services;
        }

        private static IServiceCollection ConfigureProviders(this IServiceCollection services)
        {
            services.AddSingleton<IKafkaConfigurationProvider, KafkaConfigurationProvider>();

            return services;
        }

        private static IServiceCollection ConfigureKafka(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var bootstrapServers = configuration?.GetSection("Kafka:BootstrapServers").Value ?? "";
            System.Console.WriteLine($"bootstrapServers: {bootstrapServers}");
            services.AddSingleton<IKafkaProducer>(new KafkaProducer(bootstrapServers));

            return services;
        }
    }
}
