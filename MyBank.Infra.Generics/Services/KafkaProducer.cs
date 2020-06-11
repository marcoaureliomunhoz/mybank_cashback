using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using MyBank.Infra.Generics.Interfaces;
using Newtonsoft.Json;

namespace MyBank.Infra.Generics.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly string _bootstrapServers;

        public KafkaProducer(string bootstrapServers)
        {
            _bootstrapServers = bootstrapServers;
        }

        public async Task<long> PublishMessage<T>(string topic, T message) where T : class
        {
            try
            {
                var json = JsonConvert.SerializeObject(message);
                return await PublishMessage(topic, json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> PublishMessage(string topic, string message)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = _bootstrapServers,
                    ReceiveMessageMaxBytes = (int)2000000000
                };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(
                        topic,
                        new Message<Null, string> {
                            Value = message
                        }
                    );
                    return result.Offset.Value;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
