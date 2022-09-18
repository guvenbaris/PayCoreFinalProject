using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paycore.RabbitMqConsumer;
using PayCore.Application.Enums;
using PayCore.Application.Interfaces.Mail;
using PayCore.Application.Interfaces.RabbitMQ;
using PayCore.Persistence.Services.Mail;
using PayCore.Persistence.Services.RabbitMQ;
using UnluCoProductCatalog.Persistence.Services.RabbitMQ;

static async Task Main(string[] args)
{
    var host = CreateHostBuilder(args).Build();

    var consumer = host.Services.GetRequiredService<RabbitMqConsumer>();

    var queue = RabbitMqQueue.EmailSenderQueue.ToString();

    consumer.Start(queue);
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureServices((services) =>
    {
        services.AddScoped<IConfiguration>(_ => 
        new ConfigurationBuilder().AddJsonFile($"C:/Users/Affinitybox/Desktop/GuvenBaris/Utilities/PayCoreFinal/src/Presentation/Paycore.WebAPI/appsettings.json").Build());

        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        services.AddScoped<ISmtpServer, SmtpServer>();
        services.AddTransient<RabbitMqConsumer>();
    });