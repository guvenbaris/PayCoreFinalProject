using RabbitMQ.Client;
using PayCore.Application.Interfaces.RabbitMQ;
using Microsoft.Extensions.Options;
using PayCore.Application.Utilities.Appsettings;

namespace PayCore.Persistence.Services.RabbitMQ
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IOptions<PayCoreAppSettings> _options;

        public RabbitMqService(IOptions<PayCoreAppSettings> options)
        {
            _options = options;
        }

        public IConnection GetRabbitMqConnection()
        {
            ConnectionFactory connectionFactory = new()
            {
                HostName = _options.Value.RabbitMqSettings.Host,
            };

            return connectionFactory.CreateConnection();
        }
    }
}
