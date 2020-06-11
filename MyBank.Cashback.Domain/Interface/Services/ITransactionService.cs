using System.Threading.Tasks;
using MyBank.Cashback.Domain.Entities;

namespace MyBank.Cashback.Domain.Interface.Services
{
    public interface ITransactionService
    {
        Task<Transaction> Insert(Transaction transaction);
    }
}
