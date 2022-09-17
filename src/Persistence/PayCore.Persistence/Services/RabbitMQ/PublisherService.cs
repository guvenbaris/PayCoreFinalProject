
using PayCore.Application.Dtos.Email;
using PayCore.Application.Interfaces.RabbitMQ;

namespace PayCore.Persistence.Services.RabbitMQ;

public class PublisherService : IPublisherService
{

    public void Publish(EmailToSend email,string queueName)
    {
        //using var connection = _rabbitMqService.GetRabbitMqConnection();
        //using var channel = connection.CreateModel();

        //channel.QueueDeclare(queueName, false, false, false, null);
        //var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(email));

        //channel.BasicPublish("", queueName, null, body: body);
    }
}

