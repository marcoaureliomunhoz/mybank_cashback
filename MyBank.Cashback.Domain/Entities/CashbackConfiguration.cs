using System;

namespace MyBank.Cashback.Domain.Entities
{
    public class CashbackConfiguration
    {
        public int CashbackConfigurationId { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
        public decimal BasePercentage { get; set; }
    }
}
