using PayCore.Application.Dtos.Email;

namespace PayCore.Application.Interfaces.RabbitMQ;

public interface IPublisherService
{
    void Publish(EmailToSend email, string queueName);
}
