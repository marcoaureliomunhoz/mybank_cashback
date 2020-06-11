using MyBank.Cashback.Domain.Dtos;

namespace MyBank.Cashback.Domain.Interface.Services
{
    public interface ICashbackService
    {
        void InsertCashback(NewTransactionCashbackDto dto);
    }
}
