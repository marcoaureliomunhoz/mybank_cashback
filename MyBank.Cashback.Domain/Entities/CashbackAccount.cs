using System;

namespace MyBank.Cashback.Domain.Entities
{
    public class CashbackAccount
    {
        public int CashbackAccountId { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal Value { get; set; }
        public bool Active { get; set; }
        public int ClientId { get; set; }
        public int TransactionId { get; set; }
    }
}
