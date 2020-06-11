using Microsoft.Extensions.Configuration;
using MyBank.Infra.Generics.Configurations;
using MyBank.Infra.Generics.Interfaces;

namespace MyBank.Infra.Generics.Providers
{
    public class KafkaConfigurationProvider : IKafkaConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public KafkaConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public KafkaConfiguration Get()
        {
            return new KafkaConfiguration
            {
                BootstrapServers = _configuration.GetSection("Kafka:BootstrapServers").Value,
                Topic = _configuration.GetSection("Kafka:Topic").Value,
                GroupId = _configuration.GetSection("Kafka:GroupId").Value
            };
        }
    }
}
