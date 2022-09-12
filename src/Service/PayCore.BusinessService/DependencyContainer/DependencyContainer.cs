using Microsoft.Extensions.DependencyInjection;
using PayCore.Application.Interfaces.Services;
using PayCore.BusinessService.Services;

namespace PayCore.BusinessService.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IContainerService, ContainerService>();


            return services;
        }
    }
}
