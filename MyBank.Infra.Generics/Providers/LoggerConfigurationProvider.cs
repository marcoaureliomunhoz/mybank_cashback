using System;
using System.Reflection;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace MyBank.Infra.Generics.Providers
{
    public class LoggerConfigurationProvider
    {
        public static ILogger Provides()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(GetElasticsearchSinkOptions())
                .CreateLogger();
            return Log.Logger;
        }

        private static ElasticsearchSinkOptions GetElasticsearchSinkOptions()
        {
            var elasticsearchUri = new Uri("http://localhost:9200");
            var indexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
            var options = new ElasticsearchSinkOptions(elasticsearchUri) {
                FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog | EmitEventFailureHandling.RaiseCallback,
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                IndexFormat = indexFormat,
                MinimumLogEventLevel = Serilog.Events.LogEventLevel.Verbose,
                CustomFormatter = new ExceptionAsObjectJsonFormatter()
            };
            return options;
        }
    }
}