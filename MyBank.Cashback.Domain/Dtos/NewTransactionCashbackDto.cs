namespace MyBank.Cashback.Domain.Dtos
{
    public class NewTransactionCashbackDto
    {
        public int TransactionId { get; set; }
        public decimal Value { get; set; }
        public int ClientId { get; set; }
    }
}
