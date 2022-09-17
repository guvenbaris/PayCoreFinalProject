using Microsoft.Extensions.DependencyInjection;
using PayCore.Application.Interfaces.RabbitMQ;
using PayCore.Persistence.Services.RabbitMQ;

namespace PayCore.Persistence.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IPublisherService, PublisherService>();
            return services;
        }
    }
}
