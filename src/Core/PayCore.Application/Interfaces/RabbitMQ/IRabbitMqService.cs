using RabbitMQ.Client;

namespace PayCore.Application.Interfaces.RabbitMQ
{
    public interface IRabbitMqService
    {
        IConnection GetRabbitMqConnection();
    }
}
