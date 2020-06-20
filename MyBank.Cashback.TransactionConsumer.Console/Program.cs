using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBank.Cashback.Domain.Interface.Repositories;
using MyBank.Cashback.Domain.Interface.Services;
using MyBank.Cashback.Domain.Services;
using MyBank.Cashback.Infra.Data.Repositories;
using MyBank.Cashback.TransactionConsumer.Console.Controllers;
using MyBank.Infra.Generics.Extensions;
using MyBank.Infra.Generics.Providers;

namespace MyBank.Cashback.TransactionConsumer.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var projectPath = System.IO.Path.GetDirectoryName(path);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            System.Console.WriteLine($"ProjectPath: {projectPath}");

            var connectionString = configuration?.GetConnectionString("MyBank_Cashback") ?? "";
            System.Console.WriteLine($"connectionString in ProducerConsole: {connectionString}");

            LoggerConfigurationProvider.Provides();

            var serviceProvider = new ServiceCollection()
                .ConfigureLogging()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<ICashbackRepository, CashbackRepository>()
                .AddSingleton<ICashbackService, CashbackService>()
                .AddSingleton(typeof(TransactionController))
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            if (logger != null)
                logger?.LogInformation("[{date}] TransactionConsumer Started", DateTime.UtcNow);
            else
                System.Console.WriteLine("logger not ready");

            System.Console.WriteLine("TransactionConsumer Started");

            serviceProvider
                .GetService<TransactionController>()
                .Run();
        }
    }
}
