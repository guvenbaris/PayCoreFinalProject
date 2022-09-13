using Microsoft.Extensions.DependencyInjection;
using PayCore.Application.Interfaces.Cache;
using PayCore.Application.Interfaces.Services;
using PayCore.BusinessService.Cache;
using PayCore.BusinessService.Services;

namespace PayCore.BusinessService.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IContainerService, ContainerService>();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
