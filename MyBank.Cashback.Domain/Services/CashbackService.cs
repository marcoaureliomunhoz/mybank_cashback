using MyBank.Cashback.Domain.Dtos;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;
using MyBank.Cashback.Domain.Interface.Services;

namespace MyBank.Cashback.Domain.Services
{
    public class CashbackService : ICashbackService
    {
        private readonly ICashbackRepository _cashbackRepository;
        private readonly CashbackConfiguration _cashbackConfiguration;

        public CashbackService(ICashbackRepository cashbackRepository)
        {
            _cashbackRepository = cashbackRepository;
            _cashbackConfiguration = _cashbackRepository.GetConfiguration();
        }

        public void InsertCashback(NewTransactionCashbackDto dto)
        {
            var existsTransaction = _cashbackRepository.ExistsAccountByTransaction(dto.TransactionId);

            if (!existsTransaction)
            {
                var value = _cashbackConfiguration.BasePercentage > decimal.Zero
                    ? dto.Value * (_cashbackConfiguration.BasePercentage / 100)
                    : decimal.Zero;

                _cashbackRepository.InsertAccount(new CashbackAccount
                {
                    TransactionId = dto.TransactionId,
                    ClientId = dto.ClientId,
                    Value = value
                });
            }
        }
    }
}
