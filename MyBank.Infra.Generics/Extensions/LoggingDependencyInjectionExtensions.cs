using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MyBank.Infra.Generics.Extensions
{
    public static class LoggingDependencyInjectionExtensions
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddSerilog(dispose: true));

            return services;
        }
    }
}