using System.Threading.Tasks;

namespace MyBank.Infra.Generics.Interfaces
{
    public interface IKafkaProducer
    {
        Task<long> PublishMessage<T>(string topic, T message) where T : class;
        Task<long> PublishMessage(string topic, string message);
    }
}
