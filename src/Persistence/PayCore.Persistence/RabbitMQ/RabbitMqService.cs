using PayCore.Application.Interfaces.RabbitMQ;
using RabbitMQ.Client;

namespace PayCore.Persistence.RabbitMQ
{
    public class RabbitMqService : IRabbitMqService
    {
        public IConnection GetConnection()
        {
            ConnectionFactory connectionFactory = new()
            {
                HostName = "localhost"
            };

            return connectionFactory.CreateConnection();
        }
    }
}
