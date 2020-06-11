using System;
using System.Threading.Tasks;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;
using MyBank.Cashback.Domain.Interface.Services;
using MyBank.Infra.Generics.Interfaces;

namespace MyBank.Cashback.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IKafkaProducer _kafkaProducer;
        private readonly string _topic;

        public TransactionService(
            ITransactionRepository repository,
            IKafkaProducer kafkaProducer,
            IKafkaConfigurationProvider kafkaConfigurationProvider)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
            _topic = kafkaConfigurationProvider.Get().Topic;
        }

        public async Task<Transaction> Insert (Transaction transaction)
        {
            _repository.Insert(transaction);

            try
            {
                await _kafkaProducer.PublishMessage(_topic, transaction);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"erro: {ex.Message}");
            }

            return transaction;
        }
    }
}
