using System;
using Microsoft.Extensions.Configuration;
using MyBank.Cashback.Domain.Dtos;
using MyBank.Cashback.Domain.Interface.Services;
using MyBank.Cashback.TransactionConsumer.Console.Messages;
using MyBank.Infra.Generics.Providers;
using MyBank.Infra.Generics.Services;
using Newtonsoft.Json;

namespace MyBank.Cashback.TransactionConsumer.Console.Controllers
{
    public class TransactionController 
    {
        private readonly ICashbackService _cashbackService;
        private readonly KafkaConsumer _consumer;

        public TransactionController(
            IConfiguration configuration,
            ICashbackService cashbackService)
        {
            _cashbackService = cashbackService;

            var kafkaConfiguration = new KafkaConfigurationProvider(configuration).Get();

            _consumer = new KafkaConsumer(
                kafkaConfiguration.BootstrapServers,
                kafkaConfiguration.GroupId,
                kafkaConfiguration.Topic,
                NewTransaction,
                TransactionException);

            System.Console.CancelKeyPress += (_, e) =>
             {
                 e.Cancel = true;
                 _consumer.Deactive();
             };
        }

        public void Run()
        {
            _consumer.Run();
        }

        private void NewTransaction(string message)
        {
            try
            {
                var transaction = JsonConvert.DeserializeObject<TransactionMessage>(message);
                _cashbackService.InsertCashback(new NewTransactionCashbackDto
                {
                    TransactionId = transaction.TransactionId,
                    Value = transaction.Value
                });
            }
            catch (Exception ex)
            {
                TransactionException(ex);
            }
        }

        private void TransactionException(Exception exception)
        {
            //  log in kibana
        }
    }
}
