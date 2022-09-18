using PayCore.Application.Dtos.Email;

namespace PayCore.Application.Interfaces.RabbitMQ;

public interface IPublisherService
{
    void PublishEmail(EmailToSend email, string queueName);
}
