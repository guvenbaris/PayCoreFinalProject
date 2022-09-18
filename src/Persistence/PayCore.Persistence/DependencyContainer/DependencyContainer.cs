using Microsoft.Extensions.DependencyInjection;
using PayCore.Application.Interfaces.Mail;
using PayCore.Application.Interfaces.RabbitMQ;
using PayCore.Persistence.Services.Mail;
using PayCore.Persistence.Services.RabbitMQ;
using UnluCoProductCatalog.Persistence.Services.RabbitMQ;

namespace PayCore.Persistence.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<ISmtpServer, SmtpServer>();
            services.AddSingleton<IPublisherService, PublisherService>();
            services.AddSingleton<IRabbitMqService, RabbitMqService>();

            return services;
        }
    }
}
