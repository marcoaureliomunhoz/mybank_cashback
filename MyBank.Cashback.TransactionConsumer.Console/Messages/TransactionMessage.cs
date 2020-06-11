using System;

namespace MyBank.Cashback.TransactionConsumer.Console.Messages
{
    public class TransactionMessage
    {
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal Value { get; set; }
    }
}
