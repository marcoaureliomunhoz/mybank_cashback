using System;

namespace MyBank.Cashback.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal Value { get; set; }
        public int ClientId { get; set; }
    }
}
