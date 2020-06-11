namespace MyBank.Infra.Generics.Configurations
{
    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
        public string GroupId { get; set; }
    }
}
