using System.Collections.Generic;
using MyBank.Cashback.Domain.Entities;

namespace MyBank.Cashback.Domain.Interface.Repositories
{
    public interface ICashbackRepository
    {
        CashbackConfiguration GetConfiguration();
        bool InsertAccount(CashbackAccount account);
        bool ExistsAccountByTransaction(int transactionId);
        decimal GetBalance(int clientId);
        IEnumerable<CashbackAccount> ListAll(int clientId);
  }
}
