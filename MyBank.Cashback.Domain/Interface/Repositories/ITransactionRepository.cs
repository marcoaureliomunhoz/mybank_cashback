using MyBank.Cashback.Domain.Entities;
using System.Collections.Generic;

namespace MyBank.Cashback.Domain.Interface.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> ListAll();
        bool Insert(Transaction transaction);
    }
}
