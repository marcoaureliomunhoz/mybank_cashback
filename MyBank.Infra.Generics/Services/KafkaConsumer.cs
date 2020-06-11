using System;
using System.Threading;
using Confluent.Kafka;
using MyBank.Infra.Generics.Interfaces;

namespace MyBank.Infra.Generics.Services
{
    public delegate void DelegNewMessage(string message);
    public delegate void DelegException(Exception exception);

    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly string _bootstrapServers;
        private readonly string _groupId;
        private readonly string _topic;
        private bool _active = true;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly DelegNewMessage _delegNewMessage;
        private readonly DelegException _delegException;

        public KafkaConsumer(
            string bootstrapServers, string groupId, string topic,
            DelegNewMessage delegNewMessage, DelegException delegException)
        {
            _bootstrapServers = bootstrapServers;
            _groupId = groupId;
            _topic = topic;
            _delegNewMessage = delegNewMessage;
            _delegException = delegException;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Deactive()
        {
            _active = false;
            _cancellationTokenSource.Cancel();
        }

        public void Run()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = $"{_topic}-group-{_groupId}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ReceiveMessageMaxBytes = (int)2000000000
            };

            System.Console.WriteLine("KafkaConsumer On");

            try
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(_topic);

                    try
                    {
                        while (_active)
                        {
                            System.Console.WriteLine("KafkaConsumer Consume...");
                            var result = consumer.Consume(_cancellationTokenSource.Token);
                            //var result = consumer.Consume(1000);
                            System.Console.WriteLine($"KafkaConsumer Result = {result}");
                            if (result != null)
                                _delegNewMessage?.Invoke(result.Message.Value);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"KafkaConsumer Error = {ex.Message}");
                _delegException?.Invoke(ex);
            }
        }
    }
}
